using System.Collections.Generic;

public static class MistakeList
{
    public static Dictionary<MistakeType, Mistake> mistakes = new Dictionary<MistakeType, Mistake>() {
        { 
            MistakeType.PRODURO_NOCHAO, 
            new Mistake(
                "Produto no Chão",
                "Deixou cair um produto.",
                "") 
        }, {
            MistakeType.PRODURO_CONTAMINADO,
            new Mistake(
                "Produto Contaminado",
                "Utilizou produto que tocou no chão.",
                "Qualquer produto que tenha tocado no chão deve ser descartado.")
        }, { 
            MistakeType.PRODURO_CRU, 
            new Mistake(
                "Produto Cru",
                "Colocou na gaveta um produto cru.",
                "Deve respeitar os temporizadores das frigideiras e dos grelhadores.", 0)
        }, { 
            MistakeType.PRODURO_QUEIMADO,
            new Mistake(
                "Produto Queimado",
                "Colocou na gaveta um produto queimado.",
                "Os temporizadores das frigideiras e dos grelhadores já estão programados para dar aos produtos o tempo de cozedura correto.", 0)
        }, {
            MistakeType.PRODUTO_FRIO, 
            new Mistake(
                "Produto Frio",
                "Deixou a gaveta fora da UHC muito tempo.",
                "Deve transferir rapidamente a gaveta para a UHC assim que a acabar de encher.")
        },

        {
            MistakeType.PRODUTO_MISTURADO_GRELHADOR,
            new Mistake(
                "Misturou Produto",
                "Misturou produtos de tipos diferentes sobre a mesma placa no gelhador.",
                "Deve utiliza uma placa diferente para cada tipo de produto.")
        }, {
            MistakeType.PRODUTO_MISTURADO_GAVETA,
            new Mistake(
                "Misturou Produto",
                "Misturou produto novo com produto velho.",
                "Qualquer produto que sobre nas gavetas deve ser descartado.")
        }, {
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
            MistakeType.BIFE_SAL_SEM,
            new Mistake(
                "Bife Sem Sal",
                "Colocou na gaveta uma carne que não foi salgada.",
                "Deve salgar os bifes assim que a placa superior subir.")
        }, { 
            MistakeType.BIFE_SAL_MUITO,
            new Mistake(
                "Bife Muito Salgado",
                "Salgou demasiado a carne.",
                "Deve salgar as carnes uma a uma com o movimento de martelo (Duas vezes é suficiente).")
        }, {
            MistakeType.BIFE_CRUSALGADO,
            new Mistake(
                "Bife Cru Salgado",
                "Salgou um bife que ainda estava cru.",
                "Deve salgar as carnes apenas quando a placa superior do grelhador levantar.")
        }, {
            MistakeType.BIFE_ORDEMERRADA,
            new Mistake(
                "Ordem Errada",
                "Tirou os bifes do grelhado na ordem errada.",
                "Deve retirar os bifes na ordem em que foram colocados no grelhador.")
        },



        { 
            MistakeType.FRITO_MALESCORRIDO,
            new Mistake(
                "Frito Mal Escorrido",
                "Tirou a grelha de cima da cuba muito cedo.",
                "Deve escorrer os fritos 5 a 10 segundos, só depois tirar para o seu lugar respetivo.")
        }, { 
            MistakeType.FRITO_SALGADO,
            new Mistake(
                "Frito Salgado",
                "Não deve salgar fritos.",
                "")
        },



        {
            MistakeType.GRELHADOR_FRITO,
            new Mistake(
                "Frito No Grelhador",
                "Colocou um produto frito no grelhador.",
                "O grelhador é apenas para bifes.")
        }, {
            MistakeType.GRELHADOR_SUJO,
            new Mistake(
                "Grelhador Sujo",
                "Utilizou o grelhador antes de o limpar.",
                "Deve utilizar o rodo para limpar a placa superior e inferior antes de colocar quais quer itens no grelhador."
                )
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
        }, {
            MistakeType.GAVETA_PAPEL_FALTA,
            new Mistake(
                "Falta Papel",
                "Colocou a carne numa gaveta sem papel.",
                "Deve colocar papel no fundo da gaveta antes de colocar as carnes neste.")
        }, { 
            MistakeType.GAVETA_PAPEL_MUITO,
            new Mistake(
                "Muito Papel",
                "Colocou muito papel na gaveta.",
                "Deve colocar apenas uma folha de papel no fundo da gaveta.")
        }, {
            MistakeType.GAVETA_PAPEL_SUJO,
            new Mistake(
                "Papel Sujo",
                "Utilizou uma folha de papel mais que uma vez.",
                "Deve trocar o papel sempre que reutilizar a gaveta.")
        },



        { 
            MistakeType.ITEM_CONTAMINADO,
            new Mistake(
                "Item Contaminado",
                "Utilizou um item que tocou no chão",
                "Não deve utilizar qualquer item que tenha tocado no chão, deve coloca-lo na banca da louça e procurar um item limpo.")
        },



        { 
            MistakeType.REL_GAVETAPRODUTO,
            new Mistake(
                "Gaveta Errada",
                "Colocou o produto numa gaveta de tipo não corespondente.",
                "", 1)
        }, {
            MistakeType.REL_PRODUTOSLOT,
            new Mistake(
                "Slot Errada",
                "Colocou a gaveta numa slot que não corresponde ao produto.",
                "O tipo de produto está marcado na etiqueta em cima da slot.", 1)
        },



        { 
            MistakeType.UHC_ORDEMERRADA,
            new Mistake(
                "Ordem Errada",
                "Não seguiu a gestão vertical da UHC.",
                "Deve colocar as gavetas por ordem no UHC, gavetas colocadas à mais tempo na UHC devem ser subtituidas primeiro (de cima para baixo).")
        }, { 
            MistakeType.UHC_TEMPORIZADOR_NAOACIONADO,
            new Mistake(
                "Temporizador Não Acionado",
                "Não acionou o temporizador depois de ter colocado a gaveta na slot.",
                "Para ativar o temporizador, pressione o botão ao lado da etiqueta.")
        }, { 
            MistakeType.UHC_TEMPORIZADOR_INVALIDADO,
            new Mistake(
                "Temporizador Invalidado.",
                "Reacionou o temporizador de uma slot com itens ainda dentro da validade.",
                "Acione o temporizador apenas quando for necessário.")
        },
    };



    public static string[] sufixes =
    {
        "Se achar que o equipamento em questão não está a funcionar corretamente deve chamar o gerente.",

        "Vermelho - Bife" +
        "Verde - Bife Vegan" +
        "Amarelo - Panado de Frango" +
        "Laranja - Nugget de Frango" +
        "Azul - Filete de Peixe" +
        "Roxo - Douradinhos",


    };
}
