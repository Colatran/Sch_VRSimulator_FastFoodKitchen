using System.Collections.Generic;

public static class MistakeLibrary
{
    public static Dictionary<MistakeType, Mistake> mistakes = new Dictionary<MistakeType, Mistake>()
    {
        //PRODUTO
        {
            MistakeType.PRODURO_QUEIMADO,
            new Mistake(
                "Produto Queimado",
                "Deixou produto cozinhar demais.",
                "Os temporizadores das frigideiras e dos grelhadores já estão programados para dar aos produtos o tempo de cozedura correto."
                + "\nSe achar que o equipamento em questão não está a funcionar corretamente deve chamar o gerente.")
        },
        {
            MistakeType.PRODUTO_FRIO,
            new Mistake(
                "Produto Frio",
                "Deixou a gaveta fora da UHC muito tempo.",
                "Deve transferir rapidamente a gaveta para a UHC assim que a acabar de encher.")
        },
        {
            MistakeType.PRODUTO_COMASMAOS,
            new Mistake(
                "Produto nas mãos",
                "Pegou no produto cozinhado com as mãos.",
                "Deve evitar tocar nos produtos com as mãos para evitar contaminação.")
        },

        //PRODUTOBIFE
        {
            MistakeType.PRODUTOBIFE_CRUSALGADO,
            new Mistake(
                "Bife Cru Salgado",
                "Salgou um bife que ainda estava cru.",
                "Deve salgar as carnes apenas quando a placa superior do grelhador levantar.")
        },
        {
            MistakeType.PRODUTOBIFE_MUITOSAL,
            new Mistake(
                "Bife Muito Salgado",
                "Salgou demasiado a carne.",
                "Deve salgar as carnes uma a uma com o movimento de martelo (Duas vezes é suficiente).")
        },

        //PRODUTOFRITO
        {
            MistakeType.PRODUTOFRITO_SALGADO,
            new Mistake(
                "Frito Salgado",
                "Salgou frito.",
                "Não deve salgar fritos.")
        },


        //GAVETA
        {
            MistakeType.GAVETA_PRODUTO_CONTAMINADO,
            new Mistake(
                "Produto Contaminado",
                "Colocou na gaveta um produto que tocou no chão.",
                "Qualquer produto que tenha tocado no chão deve ser descartado.")
        },
        {
            MistakeType.GAVETA_PRODUTO_MISTURADO_LOTE,
            new Mistake(
                "Produto Velho",
                "Misturou produto novo com produto velho.",
                "Qualquer produto que sobre nas gavetas deve ser descartado.")
        },
        {
            MistakeType.GAVETA_PRODUTO_CRU,
            new Mistake(
                "Produto Cru",
                "Colocou na gaveta um produto cru.",
                "Deve respeitar os temporizadores das frigideiras e dos grelhadores."
                + "\nSe achar que o equipamento em questão não está a funcionar corretamente deve chamar o gerente.")
        },
        {
            MistakeType.GAVETA_PRODUTO_QUEIMADO,
            new Mistake(
                "Produto Queimado",
                "Colocou na gaveta um produto queimado.",
                "Produtos queimados devem ser descartados.")
        },

        //GAVETABIFE
        {
            MistakeType.GAVETABIFE_PAPEL_MUITO,
            new Mistake(
                "Muito Papel",
                "Colocou muito papel na gaveta.",
                "Deve colocar apenas uma folha de papel no fundo da gaveta.")
        },
        {
            MistakeType.GAVETABIFE_PAPEL_REUTILIZADO,
            new Mistake(
                "Papel Reutilizado",
                "Utilizou uma folha de papel mais que uma vez.",
                "Deve trocar o papel sempre que reutilizar a gaveta.")
        },
        {
            MistakeType.GAVETABIFE_PAPEL_SUJO,
            new Mistake(
                "Papel Sujo",
                "Utilizou uma folha de papel suja.",
                "Folhas de papel que estiverem contaminadas devem ser descartadas.")
        },
        {
            MistakeType.GAVETABIFE_PAPEL_FALTA,
            new Mistake(
                "Falta Papel",
                "Colocou a carne numa gaveta sem papel.",
                "Deve colocar papel no fundo da gaveta antes de colocar as carnes.")
        },
        {
            MistakeType.GAVETABIFE_BIFE_TIPOERRADO,
            new Mistake(
                "Gaveta Errada",
                "Colocou um bife de um tipo não correspondente à gaveta."
                + "\nNão pode colocar produtos de tipos diferentes na mesma gaveta.",
                "\nVermelho - Bife Normal"
                + "\nVerde - Bife Vegan")
        },
        {
            MistakeType.GAVETABIFE_BIFE_SEMSAL,
            new Mistake(
                "Bife Sem Sal",
                "Colocou na gaveta uma carne que não foi salgada.",
                "Deve salgar os bifes assim que a placa superior levantar.")
        },
        {
            MistakeType.GAVETABIFE_FRITO,
            new Mistake(
                "Gaveta Errada",
                "Colocou na gaveta para bifes um produto frito."
                + " Não pode colocar produtos de tipos diferentes na mesma gaveta.",
                "As gavetas vermelhas e verdes são somente para bifes.")
        },

        //GAVETAFRITO
        {
            MistakeType.GAVETAFRITO_GRELHA_SUJA,
            new Mistake(
                "Grelha Suja",
                "Utilizou um grelha suja.",
                "Equipamento sujo devem ser limpo antes de utilizado.")
        },
        {
            MistakeType.GAVETAFRITO_GRELHA_ERRADA,
            new Mistake(
                "Grelha Errada",
                "Colocou o tipo de grelha errada na gaveta.",
                "")
        },
        {
            MistakeType.GAVETAFRITO_GRELHA_FALTA,
            new Mistake(
                "Falta Grelha",
                "Colocou produto frito numa gaveta sem grelha.",
                "Gavetas para fritos devem ter a sua grelha respetiva.")
        },
        {
            MistakeType.GAVETAFRITO_PAPEL,
            new Mistake(
                "Papel em Gaveta para Fritos",
                "Colocou papel numa gaveta para fritos.",
                "Gavetas para fritos não necessitam de papel, necessitam apenas da sua grelha respetiva.")
        },
        {
            MistakeType.GAVETAFRITO_BIFE,
            new Mistake(
                "Gaveta Errada",
                "Colocou na gaveta para fritos um bife."
                + " Não pode colocar produtos de tipos diferentes na mesma gaveta.",
                "As gavetas amarelas, laranjas, azuis e roxas são somente para fritos.")
        },
        {
            MistakeType.GAVETAFRITO_FRITO_TIPOERRADO,
            new Mistake(
                "Gaveta Errada",
                "Colocou um frito de um tipo não correspondente à gaveta."
                + "\nNão pode colocar produtos de tipos diferentes na mesma gaveta.",
                "\nAmarelo - Panado"
                + "\nLaranja - Nugget de Frango"
                + "\nAzul - Filletes de Peixe"
                + "\nRoxo - Douradinhos")
        },


        //UHC
        {
            MistakeType.UHC_TEMPORIZADOR_NAOACIONADO,
            new Mistake(
                "Temporizador Não Acionado",
                "Não acionou o temporizador depois de ter colocado a gaveta na slot.",
                "Para ativar o temporizador, pressione o botão ao lado da etiqueta.")
        },
        {
            MistakeType.UHC_TEMPORIZADOR_INVALIDADO,
            new Mistake(
                "Temporizador Invalidado.",
                "Reacionou o temporizador de uma slot com itens ainda dentro da validade.",
                "Acione o temporizador apenas quando for necessário.")
        },
        {
            MistakeType.UHC_GAVETA_TIPO,
            new Mistake(
                "Gaveta Não Correspondente",
                "Colocou a gaveta numa slot de tipo que não correspondente.",
                "O tipo de produto está marcado na etiqueta em cima da slot, cada produto tem a sua gaveta:"
                + "\nVermelho - Bife"
                + "\nVerde - Bife Vegan"
                + "\nAmarelo - Panado de Frango"
                + "\nLaranja - Nugget de Frango"
                + "\nAzul - Filete de Peixe"
                + "\nRoxo - Douradinhos")
        },
        {
            MistakeType.UHC_GAVETA_PRODUTO,
            new Mistake(
                "Produto Não Correspondente",
                "Colocou a gaveta com um produto não correspondente à slot.",
                "O tipo de produto está marcado na etiqueta em cima da slot.")
        },
        {
            MistakeType.UHC_ORDEMERRADA,
            new Mistake(
                "Ordem Errada",
                "Não seguiu a gestão vertical da UHC.",
                "Deve colocar as gavetas por ordem no UHC, gavetas colocadas à mais tempo na UHC devem ser subtituidas primeiro (de cima para baixo).")
        },

        //GRELHADOR
        {
            MistakeType.GRELHADOR_SUJO,
            new Mistake(
                "Grelhador Sujo",
                "Utilizou o grelhador antes de o limpar.",
                "Deve utilizar o rodo para limpar a placa superior e inferior antes de colocar quais quer itens no grelhador.")
        },
        {
            MistakeType.GRELHADOR_PRODUTO_CONTAMINADO,
            new Mistake(
                "Produto Contaminado",
                "Colocou no grelhador um produto que tocou no chão.",
                "Qualquer produto que tenha tocado no chão deve ser descartado.")
        },
        {
            MistakeType.GRELHADOR_PRODUTO_FRITO,
            new Mistake(
                "Frito No Grelhador",
                "Colocou um produto frito no grelhador.",
                "O grelhador é apenas para bifes.")
        },
        {
            MistakeType.GRELHADOR_PRODUTO_MISTURADO,
            new Mistake(
                "Produto Misturado no Grelhador",
                "Colocou bifes de tipos diferentes sobre a mesma placa no grelhador.",
                "Não deve cozinhar produtos de tipos diferentes sobre a mesma placa.")
        },
        {
            MistakeType.GRELHADOR_ORDEMERRADA,
            new Mistake(
                "Ordem Errada",
                "Tirou os bifes do grelhado na ordem errada.",
                "Deve retirar os bifes na ordem em que foram colocados no grelhador.")
        },

        //FRITADEIRA
        {
            MistakeType.FRITADEIRA_CESTO_BIFE,
            new Mistake(
                "Bife No Cesto",
                "Colocou bife no cesto de fritar.",
                "Se tiver com inteções de cozinhar um bife, use o grelhador.")
        },
        {
            MistakeType.FRITADEIRA_CESTO_PRODUTOMISTURADO_TIPO,
            new Mistake(
                "Misturou Produto no Cesto",
                "Misturou produtos de tipos diferentes no mesmo cesto de fritar.",
                "Os cestos de fritar podem conter apena um tipo de produto.")
        },
        {
            MistakeType.FRITADEIRA_CESTO_PRODUTOMISTURADO_LOTE,
            new Mistake(
                "Misturou Lotes",
                "Misturou produto velho com produto novo no cesto de fritar.",
                "Deve cozinhar todos os produtos de um lote de uma vez")
        },
        {
            MistakeType.FRITADEIRA_OLEO_BIFE,
            new Mistake(
                "Bife Na Cuba",
                "Colocou bife na cuba.",
                "Fritadeiras são apenas para panados, nuggets, douradinhos e filetes de peixe.")
        },
        {
            MistakeType.FRITADEIRA_OLEO_PRODUTOMISTURADO_TIPO,
            new Mistake(
                "Misturou Produto na Cuba",
                "Misturou produtos de frango e peixe na mesma cuba.",
                "Pode misturar itens na cuba apenas se forem ambos de frango ou de peixe.")
        },
        {
            MistakeType.FRITADEIRA_OLEO_PRODUTOMISTURADO_LOTE,
            new Mistake(
                "Misturou Lotes na Cuba",
                "Misturou produto velho com produto novo na cuba de fritar.",
                "Deve cozinhar todos os produtos de um lote de uma vez")
        },
        {
            MistakeType.FRITADEIRA_OLEO_NAOESCORREU,
            new Mistake(
                "Não Escorreu o Óleo",
                "Retirou o cesto de cima da fritadeira antes de escorrer o óleo.",
                "Deve escorrer o óleo durante 5 a 10 segundos antes de o retirar de cima da cuba.")
        },
        {
            MistakeType.FRITADEIRA_PRODUTO_NAOSUBMERSO,
            new Mistake(
                "Frito Não Submerso",
                "O produto não ficou completamente submerso no óleo.",
                "Agite o cesto para submergir o produto caso necessario assim nenhuma parte do produto fica mal cozinhada.")
        },
        {
            MistakeType.FRITADEIRA_PRODUTO_DIRETAMENTENACUBA,
            new Mistake(
                "Produto Diretamente Na Cuba",
                "Colocou produto diretamente na Cuba.",
                "Todos os produtos que vão para a cuba devem estar dentro de um cesto de fritar.")
        },
        {
            MistakeType.FRITADEIRA_ITEMERRADO_EQUIPAMENTO,
            new Mistake(
                "Equipamento No Óleo",
                "Deixou cair o equipamento no óleo.",
                "Deve evitar contaminar a cuba do óleo.")
        },
        {
            MistakeType.FRITADEIRA_TEMPORIZADOR_INVALIDADO,
            new Mistake(
                "Temporizador Invalidado.",
                "Reacionou o temporizador depois de comessar a cozinhar.",
                "Acione o temporizador apenas quando for necessário.")
        },

        //Batcher
        {
            MistakeType.BATCHER_COZINHOUAMAIS,
            new Mistake(
                "Cozinhou a Mais",
                "Cozinhou produto que não estava a ser pedido.",
                "Deve evitar desperdícios.")
        },



        //Preparador
        {
            MistakeType.PREPARADOR_TORRADEIRA_QUEIMADO,
            new Mistake(
                "Torrou demais",
                "Deixou o pão torrar demais.",
                "O tempo programado na torradeira é suficiente.")
        },
        {
            MistakeType.PREPARADOR_TORRADEIRA_NAOTORROU,
            new Mistake(
                "Não Torrou o Pão",
                "Utilizou pão portorrar.",
                "Deve utilizar a torradaeira.")
        },

        //Falta
        {
            MistakeType.PREPARADOR_FALTA_PAO,
            new Mistake(
                "Falta Pão",
                "Entregou pedido sem o pão.",
                "Deve respeitar as receitas nos ecrâs.")
        },
        {
            MistakeType.PREPARADOR_FALTA_QUEIJO,
            new Mistake(
                "Falta Queijo",
                "Entregou pedido sem queijo.",
                "Deve respeitar as receitas nos ecrâs.")
        },
        {
            MistakeType.PREPARADOR_FALTA_CARNE,
            new Mistake(
                "Falta Carne",
                "Entregou pedido sem o carne.",
                "Deve respeitar as receitas nos ecrâs.")
        },

        //Misturou
        {
            MistakeType.PREPARADOR_MISTUROU_PAO,
            new Mistake(
                "Misturou Pão",
                "Utilizou partes de pão de tipos diferentes.",
                "Deve respeitar as receitas nos ecrâs.")
        },
        {
            MistakeType.PREPARADOR_MISTUROU_CARNE,
            new Mistake(
                "Misturou Carne",
                "Utilizou carnes de tipos diferentes.",
                "Deve respeitar as receitas nos ecrâs.")
        },
        {
            MistakeType.PREPARADOR_MISTUROU_CARNETIPO,
            new Mistake(
                "Misturou Tipos",
                "Misturou Grelhados com Fritos.",
                "Deve respeitar as receitas nos ecrâs.")
        },
        {
            MistakeType.PREPARADOR_MISTUROU_QUEIJOS,
            new Mistake(
                "Misturou Queijos",
                "Utilizou quijos de tipos diferentes e não correspondentes à receita.",
                "Deve respeitar as receitas nos ecrâs.")
        },

        //RECEITA
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA,
            new Mistake(
                "Receita Errada",
                "O Hambúrguer entregue não corresponde a nenhuma receita.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_PAO,
            new Mistake(
                "Pão Errado",
                "O pão não corresponde há receita.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_MOLHO,
            new Mistake(
                "Molho Errado",
                "Os molhos utilizados não correspondem há receita.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_MOLHO_FALTA,
            new Mistake(
                "Falta Molho",
                "Faltam molhos no pedido.",
                "Deve respeitar as receitas nos ecrâs.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_MOLHO_MUITO,
            new Mistake(
                "Muito Molho",
                "Utilizou demaziado molho.",
                "Pressionar o butão nos contentores de molho uma vez já dá a quantidade de molho que necesssário.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_INGREDIENTES,
            new Mistake(
                "Recheios Errados",
                "O conjunto de recheios utilizados não corresponde à receita.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_INGREDIENTES_FALTA,
            new Mistake(
                "Faltam Recheios",
                "Faltam recheios no pedido que entregou.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_INGREDIENTES_MUITO,
            new Mistake(
                "Demaziados Recheios",
                "Utilizou demaziados recheios.",
                "Deve seguir as quantidades nas receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_CARNE,
            new Mistake(
                "Carne Errada",
                "A carne utilizada não corresponde à da receita.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_QUEIJO,
            new Mistake(
                "Quijo Errado",
                "O queijo utilizado não corresponde ao da receita.",
                "Deve seguir as receitas com rigorozidade.")
        },
        {
            MistakeType.PREPARADOR_RECEITA_ERRADA_DESORGANIZADO,
            new Mistake(
                "Hambúrguer Desorganizado",
                "Entregou hambúrguer com erros na ordem dos ingredientes.",
                "Deve seguir a ordem em que os ingredientes aparecem nas receitas com rigorozidade.")
        },

        {
            MistakeType.PREPARADOR_NENHUMPEDIDO,
            new Mistake(
                "Nenhum Pedido",
                "Entregou hambúrguer que não corresponde a nenhum pedido.",
                "Deve evitar desperdícios.")
        },



        {
            MistakeType.PRODURO_NOCHAO,
            new Mistake(
                "Produto no Chão",
                "Deixou cair um produto.",
                "Deve evitar evitar contaminar os produtos.")
        },
        {
            MistakeType.PRODUTO_CONTAMINADO,
            new Mistake(
                "Produto Contaminado",
                "Utilizou produto que tocou no chão.",
                "Qualquer produto que tenha tocado no chão deve ser descartado.")
        },
        {
            MistakeType.ITEM_CONTAMINADO,
            new Mistake(
                "Item Contaminado",
                "Utilizou um item que tocou no chão",
                "Não deve utilizar qualquer item que tenha sido contaminado.")
        },
    };





    public static Mistake GetMistake(MistakeType type)
    {
        Mistake mistake;
        mistakes.TryGetValue(type, out mistake);
        return mistake;
    }
}
