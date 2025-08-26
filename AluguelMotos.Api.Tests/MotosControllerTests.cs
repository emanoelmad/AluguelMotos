using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AluguelMotos.Api.Controllers;
using AluguelMotos.Infrastructure.Persistence;
using AluguelMotos.Api.Models;
using AluguelMotos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

public class MotosControllerTests
{
    [Fact]
    public async Task CadastrarMoto_DeveRetornarCreatedQuandoSucesso()
    {
        var options = new DbContextOptionsBuilder<AluguelMotosDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        var context = new AluguelMotosDbContext(options);
        var controller = new MotosController(context);

        var request = new MotorcycleCreateRequest
        {
            Year = 2023,
            Model = "Honda CG",
            Plate = "TEST1234"
        };

        var result = await controller.CadastrarMoto(request);
        Assert.IsType<CreatedAtActionResult>(result);
    }
}
