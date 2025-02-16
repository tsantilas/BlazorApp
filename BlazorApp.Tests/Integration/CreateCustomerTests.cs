using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using BlazorApp.Client.IService;
using BlazorApp.Client.Models;
using BlazorApp.Client.Pages;
using System;

namespace BlazorApp.Tests.Integration
{
    public class CreateCustomerTests : TestContext
    {
        private readonly Mock<ICustomerClientService> _mockCustomerService;

        public CreateCustomerTests()
        {
            // Create and register a mock of ICustomerClientService.
            _mockCustomerService = new Mock<ICustomerClientService>();
            Services.AddSingleton<ICustomerClientService>(_mockCustomerService.Object);
        }

        [Fact]
        public void CreateCustomerForm_RendersCorrectly()
        {
            // Arrange & Act
            var component = RenderComponent<CreateCustomer>();

            // Assert: Check that the header and form are rendered.
            Assert.Contains("Create Customer", component.Markup);

            // Check that there are 8 input fields (they have class "form-control").
            var inputs = component.FindAll("input.form-control");
            Assert.Equal(8, inputs.Count);

            // Check that both the Save (submit) and Back buttons are present.
            Assert.NotNull(component.Find("button.btn.btn-primary"));
            Assert.NotNull(component.Find("button.btn.btn-secondary"));
        }

        [Fact]
        public async System.Threading.Tasks.Task SubmittingForm_Calls_CreateCustomerAsync_And_NavigatesToHome()
        {
            // Arrange
            var component = RenderComponent<CreateCustomer>();

            // Get the FakeNavigationManager.
            var navManager = Services.GetRequiredService<NavigationManager>() as FakeNavigationManager;
            Assert.NotNull(navManager);

            // Set input values, re-finding inputs after each change.
            await component.InvokeAsync(() => component.FindAll("input.form-control")[0].Change("Test Company"));   // CompanyName
            await component.InvokeAsync(() => component.FindAll("input.form-control")[1].Change("John Doe"));         // ContactName
            await component.InvokeAsync(() => component.FindAll("input.form-control")[2].Change("123 Main St"));      // Address
            await component.InvokeAsync(() => component.FindAll("input.form-control")[3].Change("Test City"));        // City
            await component.InvokeAsync(() => component.FindAll("input.form-control")[4].Change("Test Region"));      // Region
            await component.InvokeAsync(() => component.FindAll("input.form-control")[5].Change("12345"));            // PostalCode
            await component.InvokeAsync(() => component.FindAll("input.form-control")[6].Change("Test Country"));     // Country
            await component.InvokeAsync(() => component.FindAll("input.form-control")[7].Change("123-456-7890"));     // Phone

            // Act: Submit the form.
            await component.InvokeAsync(() => component.Find("form").Submit());

            // Assert: Verify CreateCustomerAsync was called with the expected CustomerDto.
            _mockCustomerService.Verify(s => s.CreateCustomerAsync(It.Is<CustomerDto>(dto =>
                dto.CompanyName == "Test Company" &&
                dto.ContactName == "John Doe" &&
                dto.Address == "123 Main St" &&
                dto.City == "Test City" &&
                dto.Region == "Test Region" &&
                dto.PostalCode == "12345" &&
                dto.Country == "Test Country" &&
                dto.Phone == "123-456-7890"
            )), Times.Once);

            // Assert: Verify that navigation occurred.
            Assert.Equal("http://localhost/", navManager.Uri);
        }

        [Fact]
        public void SubmittingEmptyForm_DoesNotShowValidationErrors()
        {
            // Arrange
            var component = RenderComponent<CreateCustomer>();

            // Act: Submit the form without entering any data.
            component.Find("form").Submit();

            // We'll check that no common validation error marker (e.g., "validation-message") appears in the markup.
            Assert.DoesNotContain("validation-message", component.Markup, System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async System.Threading.Tasks.Task ClickingBack_NavigatesToHome()
        {
            // Arrange
            var component = RenderComponent<CreateCustomer>();
            var navManager = Services.GetRequiredService<NavigationManager>() as FakeNavigationManager;
            Assert.NotNull(navManager);

            // Act: Click the Back button, wrapping the click in InvokeAsync.
            await component.InvokeAsync(() => component.Find("button.btn.btn-secondary").Click());

            // Assert: Verify that navigation occurred (navManager.Uri should be "http://localhost/").
            Assert.Equal("http://localhost/", navManager.Uri);
        }
    }
}
