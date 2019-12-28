
# Curso . NET 

Com o migrations do ef, é possível adicionar criar arquivos de migrations apenas altearando classes C#, como no exemplo abaixo que se deseja adicionar um atributo nova em uma entidade: 

O campo adicionado foi:

`public string ImagemURL { get; set; }`
Bara adicionar os arquivos de migrations, basta dar os comandos abaixo:

`dotnet ef migrations add 'Add imagemUrl field'`
`dotnet ef database update`
 
**Adicionando dependência**: `dotnet add package ${packageName}`
    
    