using System.Collections.Generic;

public static class MistakeLibrary
{
    public static Dictionary<MistakeType, Mistake> mistakes = new Dictionary<MistakeType, Mistake>()
    {
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
                "Deve evitar tocar nos produtos com as mãos para evitar contaminação, e prevenir o risco de se queimar.")
        },

        {
            MistakeType.PRODUTO_BIFE_CRUSALGADO,
            new Mistake(
                "Bife Cru Salgado",
                "Salgou um bife que ainda estava cru.",
                "Deve salgar as carnes apenas quando a placa superior do grelhador levantar.")
        },
        {
            MistakeType.PRODUTO_BIFE_MUITOSAL,
            new Mistake(
                "Bife Muito Salgado",
                "Salgou demasiado a carne.",
                "Deve salgar as carnes uma a uma com o movimento de martelo (Duas vezes é suficiente).")
        },

        {
            MistakeType.PRODUTO_FRITO_SALGADO,
            new Mistake(
                "Frito Salgado",
                "Não deve salgar fritos.",
                "")
        },



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
                "Folhas de papel que estiverem potencialmente contaminadas devem ser descartadas.")
        },
        {
            MistakeType.GAVETABIFE_PAPEL_FALTA,
            new Mistake(
                "Falta Papel",
                "Colocou a carne numa gaveta sem papel.",
                "Deve colocar papel no fundo da gaveta antes de colocar as carnes nesta.")
        },
        {
            MistakeType.GAVETABIFE_BIFE_TIPOERRADO,
            new Mistake(
                "Gaveta Errada",
                "Colocou um bife de um tipo não correspondente à gaveta."
                + "\nNão pode colocar produtos de tipos diferentes na mesma gaveta.",
                "Vermelho para bifes normais, Verde para bifes vegan."
                + "\nVermelho - Bife"
                + "\nVerde - Bife Vegan")
        },
        {
            MistakeType.GAVETABIFE_BIFE_SEMSAL,
            new Mistake(
                "Bife Sem Sal",
                "Colocou na gaveta uma carne que não foi salgada.",
                "Deve salgar os bifes assim que a placa superior subir.")
        },
        {
            MistakeType.GAVETABIFE_FRITO,
            new Mistake(
                "Gaveta Errada",
                "Colocou na gaveta para bifes um produto frito."
                + " Não pode colocar produtos de tipos diferentes na mesma gaveta.",
                "As gavetas vermelhas e verdes são somente para bifes.")
        },



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
                "Slot Errada",
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
                "Slot Errada",
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












        {
            MistakeType.PRODURO_NOCHAO,
            new Mistake(
                "Produto no Chão",
                "Deixou cair um produto.",
                "")
        }, {
            MistakeType.PRODUTO_CONTAMINADO,
            new Mistake(
                "Produto Contaminado",
                "Utilizou produto que tocou no chão.",
                "Qualquer produto que tenha tocado no chão deve ser descartado.")
        },

        {
            MistakeType.PRODUTO_MISTURADO_FRITADEIRA,
            new Mistake(
                "Misturou Produto",
                "Misturou produtos de frango e peixe na mesma cuba.",
                "Pode misturar itens na cuba apenas se forem ambos de frango ou de peixe.")
        }, {
            MistakeType.PRODUTO_MISTURADO_CESTODEFRITAR,
            new Mistake(
                "Misturou Produto",
                "Misturou produtos de tipo diferente no mesmo cesto de fritar.",
                "Os cestos de fritar podem conter apena um tipo de produto.")
        },



        {
            MistakeType.FRITO_MALESCORRIDO,
            new Mistake(
                "Frito Mal Escorrido",
                "Tirou a grelha de cima da cuba muito cedo.",
                "Deve escorrer os fritos 5 a 10 segundos, só depois tirar para o seu lugar respetivo.")
        },







        {
            MistakeType.FRITADEIRA_NAOSUBMERSO,
            new Mistake(
                "Frito Não Submerso",
                "O produto não ficou completamente submerso no óleo.",
                "Agite o cesto para submergir o produto caso necessario assim nenhuma parte do produto fica mal cozinhada.")
        }, {
            MistakeType.FRITADEIRA_DIRETAMENTENACUBA,
            new Mistake(
                "Produto Diretamente Na Cuba",
                "Colocou produto diretamente na Cuba.",
                "Todos os produtos que vão para a cuba devem estar dentro de um cesto de fritar.")
        }, {
            MistakeType.FRITADEIRA_BIFENACUBA,
            new Mistake(
                "Bife Na Cuba",
                "Colocou bife na cuba.",
                "Fritadeiras são apenas para panados, nuggets, douradinhos e filetes de peixe.")
        }, {
            MistakeType.FRITADEIRA_ITEMNACUBA,
            new Mistake(
                "Item Invalido Na Cuba",
                "Colocou um item invalido na cuba.",
                "Apenas os cestos de fritar podem entrar em contacot com o oleo das fritadeiras.")
        }, {
            MistakeType.FRITADEIRA_NAOATIVOUTEMPORIZADOR,
            new Mistake(
                "Não Ativou Temporizador",
                "Colocou cesto na cuba e não ativou o temporizador.",
                "Sempre que colocar itens na cuba, deve ativar o temporizador.")
        },
        {
            MistakeType.FRITADEIRA_NAOESCORREUOLEO,
            new Mistake(
                "Não Escorreu o Óleo",
                "Retirou o cesto de cima da fritadeira antes de escorrer o óleo.",
                "Deve escorrer o óleo durante 5 a 10 segundos antes de o retirar de cima da cuba.")
        },



        {
            MistakeType.GAVETA_GRELHA_FALTA,
            new Mistake(
                "Falta Grelha",
                "Colocou produto frito numa gaveta sem grelha.",
                "Gavetas para fritos devem ter a sua grelha respetiva.")
        }, {
            MistakeType.GAVETA_GRELHA_ERRADA,
            new Mistake(
                "Grelha Errada",
                "Colocou o tipo de grelha errada na gaveta.",
                "Gavetas para fritos devem ter a sua grelha respetiva.")
        }, {
            MistakeType.GAVETA_PAPEL_PARAFRITOS,
            new Mistake(
                "Papel em Gaveta para Fritos",
                "Colocou papel numa gaveta para fritos.",
                "Gavetas para fritos não necessitam de papel, necessitam apenas da sua grelha respetiva.")
        },



        {
            MistakeType.ITEM_CONTAMINADO,
            new Mistake(
                "Item Contaminado",
                "Utilizou um item que tocou no chão",
                "Não deve utilizar qualquer item que tenha tocado no chão, deve coloca-lo na banca da louça e procurar um item limpo.")
        },
    };





    public static Mistake GetMistake(MistakeType type)
    {
        Mistake mistake;
        mistakes.TryGetValue(type, out mistake);
        return mistake;
    }
}
