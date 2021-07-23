# ToDoAPI

#### Implementação de uma API de uma possível aplicação de criação de tarefas.
#### A implementação não possui conexão com um banco de dados, havendo apenas duas listas que não comunicam entre si, não podendo verificar a existência ou não do usuário para fazer a utilização de cada método "to-do".

## Métodos de User e To-do:
### - User(api/User):
* Get(api/User): retorna todos os usuários existentes.
* Get(api/User/{id}: retornará o usuário, com o ID correspondente.
* Post(api/User): irá criar e retornar o usuário criado. Sendo necessário a passagem de dados que correspondem ao nome, e-mail e senha, sendo todos do tipo string.
* Patch(api/User/{id}: tornará o usuário, do ID correspondido, administrador.
* Patch(api/User/{id}/newPassword): mudará a senha do usuário, com o ID correspondente, para uma nova senha, que seja válida.
* Delete(api/User/{id}: removerá o usuário, do correspondente ID.
### - ToDo(api/User/ToDo):
* Get(api/User/ToDo/{user_id}): retorna todas as tarefas criadas pelo usuário com o ID correspondente.
* Post(api/User/ToDo/{user_id}): cria e retorna a tarefa criada pelo usuário que corresponde ao ID passado.
* Patch(api/User/ToDo/{user_id}/{todo_id}/done): determinará que a tarefa foi cumprida. Alterando a propriedade "done" para verdadeira, caso todos os IDs sejam correspondentes.
* Put(api/User/ToDo/{user_id}/{todo_id}): possibilita a mudança do título ou descrição da tarefa, caso todos os IDs sejam correspondentes.
