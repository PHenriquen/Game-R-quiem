# RÉQUIEM: ECOS DO SILÊNCIO — Game Design Document V2

> **Título internacional:** REQUIEM: ECHOES OF SILENCE
>
> **Pitch:** action roguelite 2D solitário em que Noah se move livremente, luta através de uma mão de cartas em tempo real e devolve música ao mundo conforme domina o combate.

## 1. Promessa do jogo

Réquiem precisa fazer o jogador se sentir:

- **sozinho** durante a exploração;
- **inteligente** ao perceber segredos e interpretar o mundo;
- **poderoso** ao dominar o combate.

A frase de produção é:

> **Primeiro sensação, depois conteúdo.**

A primeira versão não tenta vencer outros roguelites em quantidade. Precisa provar uma identidade reconhecível em poucos minutos.

## 2. Câmera e formato

- 2D top-down/3/4;
- Windows primeiro;
- 1280×720 como resolução-base de composição;
- pixel art moderna de alta legibilidade;
- salas autorais com pequenas variações de rota;
- sem mundo aberto;
- sem procedural complexo no vertical slice.

O personagem deve parecer pequeno diante da arquitetura, mas nunca pequeno demais para leitura de combate.

## 3. Noah

Único personagem jogável.

### Visual

- porte leve;
- cabelo branco estilizado, compacto, com pontas relaxadas e duas franjas frontais;
- roupa curta assimétrica azul-negra;
- detalhe carmesim controlado;
- pequeno sino dourado;
- fragmento azul-espectral próximo ao coração;
- arma modular: **Clamor**, legível em três formas.

Silhueta antes de detalhe. Evitar excesso de acessórios.

### Personalidade

Reservado, observador e desconfiado da própria memória.

Ele não comenta tudo que vê. Quando fala, deve ser curto e específico.

## 4. Loop

1. Noah desperta/retorna ao Santuário do Último Som.
2. Observa mudanças no espaço e escolhe a configuração inicial disponível.
3. Entra na Catedral Afogada.
4. Explora uma sala autoral.
5. Enfrenta Dissonantes em tempo real.
6. Adquire ou troca ações/cartas.
7. Encontra um evento, segredo ou Eco.
8. Enfrenta elite ou rota alternativa.
9. Enfrenta o Guardião dos Sinos.
10. Recupera um fragmento de memória ou retorna após morrer.

Não há obrigação de conversar com NPCs no hub.

## 5. Combate: princípio

**Movimento é direto. Ataque é uma decisão de mão.**

Noah se move livremente e possui uma evasão universal. As principais ações ofensivas são cartas visíveis na HUD, usadas em tempo real.

A inspiração estrutural é a combinação de ação livre com seleção rápida de ações; não reproduzir interface, economia ou gacha de jogos mobile.

### Controles iniciais

- mover: WASD / analógico;
- evasão: botão direto;
- Clamor: alternar forma por botão direto;
- cartas: quatro slots/botões;
- pausa e reorganização apenas fora da pressão imediata.

## 6. Mão e baralho

### Mão

- 4 cartas visíveis;
- usar uma carta executa a ação imediatamente;
- carta usada vai ao descarte;
- após um curto tempo de compra, outra ocupa o slot;
- baralho vazio reutiliza o descarte embaralhado.

### Por que quatro

Quatro ações são suficientes para criar leitura e combinação sem transformar a HUD em menu.

A pergunta durante a luta deve ser:

> **“Como transformo estas quatro ações numa boa sequência agora?”**

### Baralho inicial do protótipo

8 cartas:

- 2× Corte Breve;
- 2× Agulha;
- 2× Passo Fantasma;
- 2× Sino Partido.

Antes de criar dezenas de cartas, essas quatro ações precisam ser diferentes pelo toque, timing e utilidade.

### Clamor

Clamor é a arma de Noah e traduz o princípio de ressonância: uma estrutura simples muda a relação entre suas partes para produzir funções diferentes.

- **Bastão / Repouso:** forma longa de apoio, alcance e travessia;
- **Lâmina dupla:** pressão em arco e controle próximo;
- **Dois bastões:** mobilidade, sequência e futura interação com escalada.

No protótipo atual, `Q` percorre as três formas e altera sua leitura visual. Modificadores de combate e travessia permanecem fora do balanceamento até o primeiro playtest no Godot.

## 7. Quatro ações-base

### Corte Breve

Ataque seguro e rápido.

Função: conectar sequências e finalizar inimigos próximos.

### Agulha

Corte/estocada linear de médio alcance.

Função: manter pressão e atingir um alvo sem se comprometer demais.

### Passo Fantasma

Avanço ofensivo curto.

Função: reposicionar enquanto mantém fluxo.

Não substitui a evasão universal.

### Sino Partido

Ação curta, pesada e circular.

Função: impacto, interrupção e finalização.

O poder vem do pequeno silêncio/preparação antes do golpe e do hit-stop, não de uma explosão gigante.

Valores de protótipo ficam em `docs/COMBAT_SPEC.md`.

## 8. Pulso

O Pulso é a camada rítmica compartilhada por música, interface, cartas e alguns inimigos.

Ação fora do ritmo continua funcionando.

Janelas iniciais do protótipo:

- Perfeito: ±65 ms;
- Bom: ±140 ms;
- Livre: fora das janelas.

BPM inicial: 100.

### Regra de acessibilidade

O jogo precisa continuar funcional sem áudio.

O Pulso possui feedback visual e futuramente calibração de latência.

## 9. Cadência

**D → C → B → A → S → RÉQUIEM**

Cadência mede domínio do fluxo.

Sobe com:

- ações Boas e Perfeitas;
- sequências limpas;
- derrotas de inimigos;
- futuramente evasões perfeitas contra ataques reais.

Cai principalmente ao:

- receber dano;
- ficar muito tempo sem manter pressão.

Não punir cada ataque perdido com queda imediata. Isso incentiva tentativa em vez de jogo defensivo demais.

## 10. RÉQUIEM

RÉQUIEM é o grande momento de presença do combate.

Não é transformação destrutiva.

Por alguns segundos Noah parece lembrar exatamente como lutar.

Mudanças:

- postura mais confiante;
- fissuras do fragmento ficam visíveis;
- animações e VFX ficam mais limpos;
- tempo de compra pode diminuir;
- ações ganham propriedades de Eco;
- trilha recebe sua camada final.

A sensação é **controle**, não caos.

## 11. Frases e combinações

O jogo pode observar as últimas cartas usadas como uma pequena **Frase**.

Não queremos lista obrigatória de combos decorados.

Algumas sequências podem produzir respostas especiais quando executadas bem.

Primeiro teste:

**Corte Breve → Passo Fantasma → Sino Partido**

Se as três ações forem Boas ou melhores, o último impacto recebe uma resposta audiovisual especial.

Primeiro validar sensação; dano extra vem depois, se necessário.

## 12. Impacto

Ordem de importância para um golpe parecer forte:

1. antecipação;
2. animação/pose;
3. contato claro;
4. hit-stop curto;
5. som;
6. câmera;
7. partícula.

Não inverter essa ordem.

Valores iniciais:

- ataques leves: ~25–35 ms de hit-stop;
- Sino Partido: ~70 ms;
- shake máximo pequeno;
- flashes muito curtos;
- câmera especial reservada para momentos especiais.

## 13. Inimigos do slice

### Peregrino Oco

Primeiro alvo de validação.

- aproxima;
- mantém distância curta;
- telegraph claro;
- golpe frontal simples;
- deve ser legível pela silhueta mesmo sem cor.

### Cantor Partido

Segundo inimigo, somente depois de o duelo básico estar divertido.

- mantém distância;
- projétil lento;
- ataca seguindo intervalos do Pulso;
- força movimento sem poluir a arena.

### Guardião Afogado

Inimigo pesado futuro da Catedral.

- deslocamento lento;
- impacto no chão;
- exige leitura de espaço.

### Elite — Sineiro Sem Rosto

Altera expectativas de timing sem simplesmente ficar mais rápido.

## 14. Chefe — Guardião dos Sinos

O chefe deve funcionar como conclusão mecânica e narrativa da Catedral.

Padrões-base:

1. onda circular;
2. golpe pesado com atraso intencional;
3. sequência de sinos que define zonas seguras/perigosas.

O jogador atento pode encontrar a mesma sequência musical antes da luta e reconhecer parte do padrão.

A metade final acrescenta intensidade musical e comportamento do cenário, não dez fases novas.

## 15. Catedral Afogada

Tema narrativo: **apego**.

Identidade:

- arquitetura gótica parcialmente submersa;
- pedra azul-escura;
- água negra com reflexos espectrais;
- bronze e sinos envelhecidos;
- vitrais quebrados;
- luz dourada rara indicando memória preservada;
- grande escala arquitetônica em salas compactas.

### Primeira sala autoral — Nave Silenciosa

Deve conter:

- área segura para entender movimento;
- primeiro encontro com Peregrino Oco;
- um sino quebrado que reage ao Pulso;
- uma porta distante como objetivo visual;
- uma interação opcional que introduz a ideia de segredo.

## 16. Santuário do Último Som

O Santuário é pequeno e vazio.

Não é uma cidade-hub.

Elementos:

- altar do Coração;
- espaço de cartas/Partitura;
- mural ou superfície onde Ecos aparecem;
- um sino principal;
- objetos que mudam de posição/estado conforme a história avança;
- passagens que podem surgir depois.

O Santuário precisa transmitir progresso através do próprio espaço.

## 17. Narrativa

A história é contada prioritariamente por:

1. cenário;
2. inimigos;
3. objetos;
4. som;
5. Ecos curtos;
6. texto.

Noah é a única presença aliada clara durante quase toda a experiência.

Uma segunda pessoa aparece indiretamente ao longo da história e fisicamente apenas perto do final.

A pergunta inicial é se Noah causou ou tentou impedir o colapso do Coração. A pergunta maior é por que ele escolheu esquecer.

Ver `docs/STORY.md`.

## 18. Lições e mensagens

Temas não são falas motivacionais.

Eles aparecem em situações e consequências.

Exemplos de assuntos:

- apego;
- memória;
- culpa;
- identidade;
- mudança;
- aceitar o passado sem precisar restaurá-lo exatamente.

Se uma cena consegue comunicar a mensagem sem explicação, cortar a explicação.

## 19. Segredos e Easter eggs

### Pequenos

Referências visuais discretas que continuam funcionando mesmo para quem não reconhece a origem.

### Mecânicos

Sinos, caminhos, cartas e ações podem ser combinados para revelar salas.

### Narrativos

Os mais valiosos. Um segredo pode mudar a interpretação de uma memória sem fornecer item nenhum.

Evitar referências que dependam de personagens, logos, músicas ou assets protegidos de terceiros.

## 20. Música adaptativa

A Catedral precisa primeiro de **uma faixa excelente**, dividida em stems/camadas sincronizadas.

Direção inicial:

1. ambiente/ruído de sala;
2. piano ou cordas muito contidos;
3. pulso/percussão;
4. sinos/textura harmônica;
5. camada RÉQUIEM.

Cadência adiciona presença sem reiniciar a música.

A exploração pode reduzir elementos até deixar apenas ambiente e fragmentos de motivo.

## 21. Arte

Paleta funcional:

- Night black `#090B12`;
- Ivory `#E9E2D0`;
- Spectral blue `#54C7CE`;
- Aged gold `#C4A35A`;
- Crimson `#9E1738`;
- Veil violet `#6651A6`.

Cor possui significado. Não usar todas as cores em todo lugar.

Regra:

> **Bonito parado, legível em movimento.**

Detalhes ambientais nunca podem esconder telegraphs.

## 22. Progressão

Vertical slice:

- baralho pequeno;
- ações desbloqueadas de forma controlada;
- escolhas durante a run apenas suficientes para testar variedade;
- Ecos narrativos persistem entre tentativas.

Sem grind pesado ou árvore enorme.

## 23. Telemetria local

Registrar em JSON:

- cartas usadas;
- Livre/Bom/Perfeito;
- dano dado/recebido;
- tempo por faixa de Cadência;
- mortes;
- duração;
- sequências comuns.

Sem envio de rede.

Objetivo: balancear usando sessões reais, não intuição pura.

## 24. Acessibilidade

Planejar desde cedo:

- Pulso visual;
- calibração de latência;
- remapeamento;
- suporte a controle;
- redução de flashes/shake;
- informação essencial nunca apenas por cor ou apenas por áudio.

## 25. Escopo do vertical slice

### Fase A — Combat toy

- Noah provisório;
- Nave Silenciosa provisória;
- movimento;
- evasão;
- mão de 4 cartas;
- baralho de 8;
- 4 ações;
- Pulso;
- Cadência;
- RÉQUIEM;
- Peregrino Oco.

### Fase B — Sala autoral

- arte-base coerente;
- Cantor Partido;
- som adaptativo;
- primeiro segredo;
- primeiro Eco;
- transição Santuário → Catedral.

### Fase C — Slice completa

- 5–6 salas;
- 3 inimigos;
- 1 elite opcional;
- Guardião dos Sinos;
- 8–12 cartas totais, dependendo dos testes;
- save básico;
- 10–15 minutos;
- teclado + controle;
- build Windows.

## 26. Fora do escopo agora

- multiplayer;
- mobile;
- gacha;
- múltiplos protagonistas;
- várias armas;
- hub cheio de NPCs;
- mundo aberto;
- procedural avançado;
- dezenas de cartas;
- múltiplas regiões completas;
- dublagem extensa;
- árvore de habilidade gigante.

## 27. Critério de identidade

Antes de produzir muito conteúdo, deve ser possível mostrar 30 segundos mudos do jogo e reconhecer:

- Noah;
- as quatro cartas;
- o Pulso;
- a escalada de Cadência;
- impacto controlado;
- solidão e arquitetura de Vesper.

Se isso ainda parecer apenas “outro roguelite pixel art”, o problema é direção, não falta de conteúdo.
