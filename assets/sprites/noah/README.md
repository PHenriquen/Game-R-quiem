# Noah — sprite prototype V1

Folha inicial para validar a presença de Noah em movimento antes da produção de animação final.

## Grade

- 4 colunas × 3 linhas;
- linha 1: 4 frames de idle;
- linhas 2–3: 8 frames de corrida;
- cada célula: 192 × 192 px;
- fundo transparente;
- direção-base: direita, com espelhamento horizontal no jogo.

## Uso no protótipo

`CombatPrototype.Sprite.cs` seleciona os frames e mantém o desenho procedural anterior como fallback caso a textura não seja carregada.

## Limitações conhecidas

- corrida vertical ainda reutiliza a leitura lateral;
- mãos da Clamor usam âncoras aproximadas;
- proporções e timing ainda precisam de playtest no Godot;
- este arquivo é um asset original de pré-produção, não arte final.

Não substituir ou recortar silenciosamente este arquivo. Versões refinadas devem receber nomes versionados.
