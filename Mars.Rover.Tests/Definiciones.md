## Contexto
La NASA planea enviar un equipo de exploradores robóticos a una meseta en Marte.

Los rovers deberán recorrer esta meseta, curiosamente rectangular, para que sus cámaras de a bordo puedan obtener una visión completa del terreno circundante para enviarla a la Tierra.

La posición de un rover se representa mediante una combinación de coordenadas x e y, y una letra que representa uno de los cuatro puntos cardinales. La meseta está dividida en una cuadrícula para simplificar la navegación. Un ejemplo de posición podría ser 0, 0, N, lo que significa que el rover se encuentra en la esquina inferior izquierda y orientado al norte.

Para controlar un rover, la NASA envía una simple secuencia de letras. Las posibles letras son «L», «R» y «M». «L» y «R» hacen que el rover gire 90 grados a la izquierda o a la derecha, respectivamente, sin moverse de su posición actual.

'M' significa avanzar un punto de la cuadrícula y mantener el mismo rumbo.


## Definiciones
* El rover es un robot que ocupa el espacio de una celda de la cuadricula, no hace falta definir su tamaño.
* El tamaño de la cuadricula es un espacio definido de 10 x 10, iniciando en 0,0. 
* El rover puede definir su posición inicial en la cuadricula,
* El rover puede girar sobre su eje viendo a 4 direcciones: Norte, Este, Sur y Oeste. Los comandos de giro son L-R. 
* El rover tiene un comando para avanzar una celda, el comando es M.
* El rover no puede moverse hacia atrás, por lo que para devolverse tiene que girar dos veces y avanzar adelante.
* El rover solo puede recibir un maximo de 10 comandos por exploración. 
* En la cuadricula solo puede estar un rover a la vez. 
* Una cuadricula la pueden explorar maximo 3 rovers en secuencias.

## Conceptos
Un rover es un vehículo robótico que se usa para explorar la superficie de otros planetas, lunas u otros cuerpos celestes. También se le conoce como astromóvil. 