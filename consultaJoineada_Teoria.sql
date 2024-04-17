select * from POKEMONS
select * from ELEMENTOS
use POKEDEX_DB

-- consulta joineada (consulta con relacion entre tablas)
Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion As Tipo, D.Descripcion As Debilidad From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo and D.Id = P.IdDebilidad

-- ELEMENTOS E, ELEMENTOS D son la misma tabla, de la 1ra vamos a traer el Tipo, y de la 2da 
-- traemos la Debilidad

