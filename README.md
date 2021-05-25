# RPG

## Projeto realizado para o aprendizado sobre as bases de um RPG, visando em sua maior parte a interação do player com objetos da cena, e criação de um inventário, onde objetos pegáveis na cena irão para esse inventário e poderão ser equipados ou utilizados pelo player, além de implementação de um sistema de combate e integrações de modelos de itens e personagens feitos no blender com os modelos já inseridos na unity.

Sobre:
===========================
-Geral:

O projeto foi realizado com base em um tutorial disponibilizado no YouTube pelos canais Brackeys e Sebastian Lague.

-Pontos de Aprendizado:

O projeto consta com a implementação de um sistema de identificação de objetos por meio da utilização de interfaces, onde os objetos que possuírem a interface interactable são os objetos que podem sofrer interação do player, com isso os objetos podem ter suas próprias ações individuais quando sofrerem uma interação, como serem adicionados ao inventário, serem utilizados ou sofrerem um ataque do player (inimigos).

O sistema de inventário controla tanto a UI(que possibilita o player visualizar os itens do inventário e selecioná-los, para removê-los ou equipá-los), quanto os itens em si, que terão diferentes formas de serem utilizados, como os equipamentos de proteção e as armas. Os itens são criados e salvos por meio de scriptable objects, que possuem todas as informações do item, como nome, dano base, defesa base e bônus de dano ou defesa, e esses por meio do sistema de equipamento são atribuídos aos status do player quando equipados, ou retirados quando desequipados, por esse mesmo sistema o mesh respectivo ao item que está sendo utilizado é adicionado ou retirado do player.

Para a movimentação tanto do player quanto dos inimigos foi utilizado o navigational mesh, onde para o player com o ponto que for clicado no mapa, e que esse possa ser acessado por ele, será realizado o pathfinding por meio do nav mesh agent para levar o player até o destino.

-Assets:

Todo os assets utilizados neste projeto foram baixados em:

- https://devassets.com/assets/rpg-tutorial-assets/


