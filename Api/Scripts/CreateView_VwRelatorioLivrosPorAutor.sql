-- View para Relatório de Livros por Autor
CREATE OR REPLACE VIEW "VwRelatorioLivrosPorAutor" AS
SELECT 
    a."Nome" AS "NomeAutor",
    l."Titulo",
    l."Editora",
    l."Edicao",
    l."AnoPublicacao",
    STRING_AGG(DISTINCT ass."Descricao", ', ' ORDER BY ass."Descricao") AS "Assunto",
    MAX(CASE WHEN tv."Descricao" = 'Balcão' THEN lv."Valor" END) AS "ValorBalcao",
    MAX(CASE WHEN tv."Descricao" = 'Internet' THEN lv."Valor" END) AS "ValorInternet",
    MAX(CASE WHEN tv."Descricao" = 'Evento' THEN lv."Valor" END) AS "ValorEvento",
    MAX(CASE WHEN tv."Descricao" = 'Self-Service' THEN lv."Valor" END) AS "ValorSelfService"
FROM 
    "Livros" l
    INNER JOIN "LivroAutores" la ON l."CodL" = la."Livro_CodL"
    INNER JOIN "Autores" a ON la."Autor_CodAu" = a."CodAu"
    LEFT JOIN "LivroAssuntos" las ON l."CodL" = las."Livro_CodL"
    LEFT JOIN "Assuntos" ass ON las."Assunto_CodAs" = ass."CodAs"
    LEFT JOIN "LivroValores" lv ON l."CodL" = lv."Livro_CodL"
    LEFT JOIN "TiposVenda" tv ON lv."TipoVenda_CodTv" = tv."CodTv"
GROUP BY 
    a."CodAu", a."Nome", l."CodL", l."Titulo", l."Editora", l."Edicao", l."AnoPublicacao"
ORDER BY 
    a."Nome", l."Titulo";