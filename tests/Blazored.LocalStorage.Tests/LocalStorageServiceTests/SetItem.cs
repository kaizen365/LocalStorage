﻿using Blazored.LocalStorage.JsonConverters;
using Blazored.LocalStorage.Tests.Mocks;
using FluentAssertions;
using Moq;
using System.Text.Json;
using Xunit;

namespace Blazored.LocalStorage.Tests.LocalStorageServiceTests
{
    public class SetItem
    {
        private JsonSerializerOptions _jsonOptions;
        private Mock<JSRuntimeWrapper> _mockJSRuntime;
        private LocalStorageService _sut;

        private static string _key = "testKey";

        public SetItem()
        {
            _mockJSRuntime = new Mock<JSRuntimeWrapper>();
            _jsonOptions = new JsonSerializerOptions();
            _jsonOptions.Converters.Add(new TimespanJsonConverter());
            _sut = new LocalStorageService(_mockJSRuntime.Object);
        }

        [Fact]
        public void Should_OverwriteExistingValue()
        {
            // Arrange
            string existingValue = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbmlzdHJhdG9yIiwiZXhwIjoxNTg1NjYwNzEyLCJpc3MiOiJDb2RlUmVkQm9va2luZy5TZXJ2ZXIiLCJhdWQiOiJDb2RlUmVkQm9va2luZy5DbGllbnRzIn0.JhK1M1H7NLCFexujJYCDjTn9La0HloGYADMHXGCFksU";
            string newValue = "﻿6QLE0LL7iw7tHPAwold31qUENt3lVTUZxDGqeXQFx38=";

            _mockJSRuntime.Setup(x => x.Invoke<string>("localStorage.getItem", new[] { _key }))
                          .Returns(() => existingValue);
            _mockJSRuntime.Setup(x => x.InvokeVoid("localStorage.setItem", new[] { _key, newValue }));

            // Act
            _sut.SetItem(_key, newValue);

            // Assert
            _mockJSRuntime.Verify();
        }
    }
}
