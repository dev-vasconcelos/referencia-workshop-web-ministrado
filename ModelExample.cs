using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiExemplo.Models {
   
  [Table("tb_usuario")] // annotation que define o da classe no banco de dados
  public class Usuario {
    
    [Key] // annotation que define esta variável como PK(Primary Key)
    [Column("id")]// annotation que define o nome desta variável no banco de dados
    public long Id {get; set;}

    [Column("name")]
    public string Name {get; set;}

    [Column("data_nascimento")]
    public DateTime DataNascimento {get; set;}
  
  }
}
