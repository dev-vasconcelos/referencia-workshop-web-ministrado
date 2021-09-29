namespace ApiExemplo.Service {
    
    public class AbstractServiceNpgsql<T> : IService<T> where T : AbstractEntity {
    
        protected IRepository<T> _repository; // Inicialização do repository
        protected NpgContext _context; // Inicialização dinâmica do contexto

        public AbstractServiceNpgsql(NpgContext context) {
            this._context = context;
            _repository = new RepositoryNpgsql<T>(context);
        }

        public AbstractServiceNpgsql() {
        
        }

        public virtual T Save(T entity) {
            _repository.Save(entity);
            
            return entity;
        }

        public virtual T Update(T entity) {
            _repository.Update(entity);

            return entity;
        }

        public virtual List<T> Get() => _repository.Get();
        public virtual T Get(T entity) => _repository.Get(entity);
