# RÉQUIEM — Asset Registry

Este arquivo existe para impedir que assets temporários virem dependências sem origem/licença conhecida.

## Regras

Para qualquer asset externo, registrar antes de considerar uso final:

- nome;
- autor/fonte;
- página de origem;
- licença;
- arquivo(s) usado(s);
- alterações feitas;
- status: candidato / protótipo / final / removido.

Assets externos podem acelerar blockout. Eles **não** definem a identidade final de Nox, Dissonantes, UI, logo ou símbolos narrativos.

## Assets atuais

### Áudio procedural do repositório

- Fonte: `tools/generate_prototype_audio.py`
- Autor: projeto Réquiem
- Licença/origem: original gerado localmente pelo script do repositório
- Uso: timing, stems e mix adaptativo provisório
- Status: **protótipo**
- Observação: não é trilha final.

## Candidatos CC0 para blockout

### Kenney — Tiny Dungeon

- Fonte: Kenney
- Página: `https://kenney.nl/assets/tiny-dungeon`
- Categoria: 2D / pixel / dungeon
- Tile size informado pela fonte: 16×16
- Licença informada pela fonte: **Creative Commons CC0**
- Uso possível: colisão, leitura de sala, objetos temporários
- Status: **candidato; ainda não importado**
- Observação: escala menor que a direção visual de Réquiem; preferir para placeholder, não para aparência final.

### Kenney — Scribble Dungeons

- Fonte: Kenney
- Página: `https://kenney.nl/assets/scribble-dungeons`
- Categoria: 2D / top-down / dungeon
- Tile size informado pela fonte: 64×64
- Licença informada pela fonte: **Creative Commons CC0**
- Uso possível: blockout de composição e escala
- Status: **candidato; ainda não importado**
- Observação: estética de rascunho pode ser útil justamente por não ser confundida com arte final.

## Preferência atual

Para o primeiro combat toy, **nenhum asset externo é necessário**. A arena usa formas desenhadas por código.

Quando entrarmos na Nave Silenciosa autoral, Scribble Dungeons é o candidato mais interessante para blockout porque comunica claramente “temporário” e reduz o risco de a equipe se apegar a um tileset genérico.

## Antes de publicar build

Checklist:

- [ ] nenhum arquivo sem origem conhecida;
- [ ] licença compatível revisada;
- [ ] atribuição adicionada quando exigida;
- [ ] arquivos temporários marcados;
- [ ] logos/personagens/músicas de outras IPs não incluídos;
- [ ] assets finais originais identificados.
