using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiExemplo.Models {
   
  [Table("tb_usuario")]
  public class Usuario {
    [Key]
    [Column("id")]
    public long Id {get; set;}

    [Column("name")]
    public string Name {get; set;}

    [Column("data_nascimento")]
    public DateTime DataNascimento {get; set;}
  
  }
}
