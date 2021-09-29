namespace ApiExemplo.Controllers {

    [ApiController] // Declaramos que esta classe é um controller
    [Produces("applcation/json")] // Declaramos o tipo de respose que a API vai retornar na rede
    [Route("api/[controller]")] //Declaramos a rota de sua URL, neste caso "IP:PORTA/api/NomeController"
    public abstract class AbstractController<T, DTO> : ControllerBase, IController<T, DTO> where T : AbstractEntity where DTO : AbstractModelDTO<T, DTO> {
        
        protected AbstractServiceNpgsql<T> _service; 
        protected NpgContext _contexto;
        protected Microsoft.Extensions.Configuration.IConfiguration _configuration {get;}
        
        public AbstractController(NpgContext contexto, Microsoft.Extensions.Configuration.IConfiguration configuration) {
            _configuration = configuration; // Instanciação do configuration que trás as configurações e especificações da API
            this._contexto = contexto;  //Instanciação dinâmica do contexto
            this._service = new AbstractServiceNpgsql<T>(contexto); //Instanciação do service
        }

        public AbstractController(NpgContext contexto) {
            this._contexto = contexto;
            this._service = new AbstractServiceNpgsql<T>(contexto);
        }

        //todas as annotations aqui representam as respostas e métodos desta requisição
        //podemos ler que esta função é chamada pelo método GET e pode dar uma resposta dentro dos códigos listados
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public virtual IActionResult Get() {
            ResponseEntity<IEnumerable<T>> response;

            try {
                response = new ResponseEntity<IEnumerable<T>>(_service.Get()); //Chama o service e transforma a resposta do mesmo para 

            } catch (Exception ex) {
                response = new ResponseEntity<IEnumerable<T>>(new List<T>(), ex); //Construi uma resposta de erro
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public virtual IActionResult Post([FromBody] DTO dto)
        {
            ResponseEntity<T> response;

            var entity = dto.ConvertFromDto(dto); //Converte o DTO recebido em Modelo, DTO será visto mais para frente
            
            try {

            } catch (NullReferenceException) {
                response = new ResponseEntity<T>(entity, "Informações nulas");
                return StatusCode(500, response);
            } catch (ArgumentNullException ex) {
                response = new ResponseEntity<T>(entity, ex.Message);
                return StatusCode(500, response);
            } catch (Exception ex) {
                response = new ResponseEntity<T>(entity, ex);
                return StatusCode(500, response);
            }

            response = new ResponseEntity<T>(entity);
            return StatusCode(500, response);
        }
