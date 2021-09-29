
namespace ApiExemplo.Repository {

public class RepositoryNpgsql<T> : IRepository<T> where T : AbstractEntity {
        protected NpgContext _context;

        //No construtor de Repository, pega-se o contexto dinâmicamente.
        //Mais informações no material do workshop
        public RepositoryNpgsql(NpgContext context) { 
            _context = context;
        }

        public virtual T Save(T entity) { 
            try {
            
                _context.Add(entity); // Adiciona uma entidade ao contexto da API atual
                _context.SaveChanges(); // Persiste as mudanças do contexto atual com o BD
            
            } catch (System.Exception ex) {
                throw new Exception("Falha ao tentar salvar a entidade", ex);
            }

            return entity;
        }

        public virtual T Update(T entity) {
            try {
                _context.Attach<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //Pega o estado de uma entidade no contexto e atualiza os seus dados
                _context.Entry<T>(entity).Property(x => x.ScopeId).IsModified = false;

                _context.SaveChanges();
            } catch (System.Exception ex) {
                throw new Exception("Falha ao tentar atualizar entidade", ex);
            }

            return entity;
        }
