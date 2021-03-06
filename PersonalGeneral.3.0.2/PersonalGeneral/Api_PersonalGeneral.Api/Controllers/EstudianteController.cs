//cSpell: disable

using System;
using AutoMapper;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using Api_PersonalGeneral.Domain.Entities;
using Api_PersonalGeneral.Domain.Interfaces;
using Api_PersonalGeneral.Domain.DTOS.requests;
using Api_PersonalGeneral.Domain.DTOS.responses;
using Api_PersonalGeneral.Infraestructure.Repositories;

namespace Api_PersonalGeneral.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
        public class EstudianteController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<InscripcionRequest> _createValidator;
        private readonly IEstudianteService _service;
        private readonly IestudianteInterface _repository;

        public EstudianteController(IestudianteInterface repository, IHttpContextAccessor httpContext, IMapper mapper, IEstudianteService service, IValidator<InscripcionRequest> createValidator)
        {
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._service = service;
            this._createValidator = createValidator;
        }

        //*El alumno busca todos los cursos 
        [HttpGet]
        [Route("TodosLosCursos")]
        public async  Task<IActionResult> ATodosLosCursos()
        {
            var cursos = await _repository.ATodosLosCursos();
            var answerCursos = _mapper.Map<IEnumerable<Curso>,IEnumerable<CursoResponses>>(cursos);
            return Ok(answerCursos);
        } 

        //*El alumno busca un curso por el id

        [HttpGet]
        [Route("Curso/{id:int}")]
        public async Task<IActionResult> ACursoPorId(int id)
        {
            var Acurso = await _repository.ACursoPorId(id);

            if(Acurso == null)
                return NotFound("Lo sentimos, el curso no fue encontrado.");

            var respuesta = _mapper.Map<Curso, CursoResponses>(Acurso);

            return Ok(respuesta);
        }

        //*El estudiante podra modificar su informaci??n 
        [HttpPut]
        [Route("ModificarInfo/{id:int}")]
        public async Task<IActionResult> Update (int id,[FromBody]Estudiante estudiante)
        {
            if(id <= 0)
                return NotFound("No se encontro su cuenta");
            
            estudiante.IdEstudiante = id;

            var Validated = _service.ActualizarEstudiante_Validated(estudiante);

            if(!Validated)
                UnprocessableEntity("No es posible actualizar su informaci??n.");

            var updated = await _repository.Update(id, estudiante);

            if(!updated)
                Conflict("Ocurrio un fallo al intentar actualizar su informaci??n.");
            
            return Ok(estudiante);
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Inscribirse(InscripcionRequest inscripcion)
        {
            var Val = await _createValidator.ValidateAsync(inscripcion);

            if(!Val.IsValid)
            
                return UnprocessableEntity (Val.Errors.Select(d => $"{d.PropertyName} => Error: {d.ErrorMessage}"));

            var entity = _mapper.Map<InscripcionRequest, Inscripcion>(inscripcion);

            var id = await _repository.Inscribirse(entity);
            
            if(id <= 0)
                return Conflict($"??ERROR!: Ocurrio un conflicto con la informaci??n...Intentelo nuevamente.");

            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/Estudiante/{id}";
            return Created(urlResult, id);
        }

        //*El alumno puede buscar su informaci??n 
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> EstudiantePorId(int id)
        {
            var estudiante = await _repository.EstudiantePorId(id);

            if(estudiante == null)
                return NotFound("Lo sentimos, su cuenta no fue encontrada.");

            //var respuesta = _mapper.Map<Estudiante, EstudianteResponses>(estudiante);

            return Ok(estudiante);
        }


        //*Dar de baja del curso (Eliminar inscripci??n)
        [HttpDelete]
        [Route("CancelarInscripcion/{id:int}")]
        public IActionResult EliminarInscripcion(int id)
        {
            _repository.EliminarInscripcion(id);

            var MessageResult = "Te has dado de baja del curso.";

            return Ok(MessageResult);
        }

    
    }
}