# Comentarios Peblo Práctica 2 Móviles
- El juego en Android se cuelga si se hace una build con IL2CPP.

Mirando el logcat, salen errores de UnityAnalytics:
<fecha> 10096 10124 E Unity   : Unable to find type [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.Analytics.AnalyticsSessionInfo
<fecha> 10096 10124 E Unity   : Unable to find method CallIdentityTokenChanged in [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.Analytics.AnalyticsSessionInfo
<fecha> 10096 10124 E Unity   : Unable to find method CallSessionStateChanged in [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.Analytics.AnalyticsSessionInfo
<fecha> 10096 10124 E Unity   : Unable to find type [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.Analytics.AnalyticsSessionState
<fecha> 10096 10124 E Unity   : Unable to find type [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.Analytics.ContinuousEvent
<fecha> 10096 10124 E Unity   : Unable to find method RemoteConfigSettingsUpdated in [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.RemoteConfigSettings
<fecha> 10096 10124 E Unity   : Unable to find type [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.RemoteConfigSettingsHelper/Tag
<fecha> 10096 10124 E Unity   : Unable to find method RemoteSettingsBeforeFetchFromServer in [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.RemoteSettings
<fecha> 10096 10124 E Unity   : Unable to find method RemoteSettingsUpdateCompleted in [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.RemoteSettings
<fecha> 10096 10124 E Unity   : Unable to find method RemoteSettingsUpdated in [UnityEngine.UnityAnalyticsModule.dll]UnityEngine.RemoteSettings

- ~~El menú de selección del lote aparece a mitad cuando se lanza la escena, en lugar de situado arriba del todo.~~

- ~~El menú de selección del lote se ajusta a los laterales en exceso.~~

- Revisar los marcos.

- Los bordes de los tiles son muy gruesos. Los "sumideros" no aparecen centrados correctamente en sus celdas.

- Las líneas de los flujos son muy gruesas y no son "continuas" (el grosor cambia ligeramente en el centro de los tiles).

- No hay animaciones de feedback al empezar o terminar un flujo

- No se muestra la estrella o el "tick" junto al tamaño del tablero cuando se vuelve a jugar un nivel ya superado.

- Si se tiene un flujo a medias de construir no siempre se "reinicia" si se arrastra en posiciones anteriores no contiguas. Solo se puede cambiar el flujo en construcción yendo hacia atrás por el mismo camino, no se puede saltar.

- Si se da al aspa de cierre del cuadro de diálogo en lugar del botón de pasar al siguiente nivel se puede seguir con el nivel recién jugado, que no parece tener mucho sentido.

- ~~Cuando se rompe una pista al crear otro flujo, se quitan las estrellas. Si se rectifica y se evita el corte, el flujo de la pista original vuelve a salir, pero las estrellas no. Sí salen las estrellas si se crea el flujo de nuevo a mano.~~

- Si se está creando un flujo, se vuelve al punto de partida y no se suelta ¡no se puede volver a crear el flujo! Al salir del "sumidero" el flujo no se crea. (A Rulo le funka asi que no se a que se refiere ¿?)

- Los bordes del tablero no se muestran bien si el nivel tiene "huecos", como en el primer nivel de Rectangles - Hourglass pack

- ~~Si se pide una pista y se cierra el juego o se da a volver (sin superar el nivel) ¡la pista se recupera!~~

- ~~Si se ve un anuncio recompensado, se consigue una pista y se cierra el juego o se da a volver (sin superar el nivel) ¡la pista conseguida por el anuncio desaparece!~~

- ~~En la escena de selección de nivel dentro del lote no se indica con claridad que los niveles están bloqueados (con el candado usado en el juego original)~~

- ~~El juego no se adapta correctamente a distintas resoluciones, requisito indispensable de la práctica:~~
    - ~~En relación de aspecto 1:1 (cuadrado):~~
        - ~~El scroll view de los lotes (menú principal) no baja más allá de "10x10 Manías", lo que impide jugar los niveles de la categoría "Rectangle"~~
        - ~~Además, el banner tapa parcialmente la última opción, por lo que dependiendo de la resolución podría incluso quedar prácticamente tapado.~~
        - ~~En la escena de selección del nivel dentro del lote, de las "páginas" de 30 niveles solo se ven las filas primeras filas (niveles 1-15) porque el resto quedan fuera de la pantalla. Además, la última fila (niveles 11-15), dependiendo de la resolución puede quedar prácticamente tapada, impidiendo jugar también esos niveles.~~
        - ~~En la escena del juego, el tablero se solapa con el HUD y el banner tapa parcialmente, dependiendo de la resolución, los botones inferiores.~~
    - ~~En relación de aspecto 2:3 con baja resolución (400x600)~~
        - ~~El scroll view de los lotes (menú principal) no baja más allá de "Rectangle Pack", lo que impide jugar al lote "Hourglass Pack"~~
        - ~~Además, el banner tapa parcialmente la última opción.~~
        - ~~En la escena de selección del nivel dentro del lote salen todas las filas pero la última queda parcialmente tapada por el banner.~~
    - ~~En relación de aspecto 2:3 con baja resolución (1536x2048, iPad 4)~~
        - ~~El scroll view de los lotes (menú principal) no baja más allá de "Tower Pack", lo que impide jugar a los lotes "Rectangle Pack" y "Hourglass Pack"~~
        - ~~Además, el banner tapa parcialmente la última opción.~~
        - ~~En la escena de selección del nivel dentro del lote salen todas las filas pero la última está cortada y no se ven los números.~~

- ~~La forma de poner las cabeceras de color letra por letra es mejorable. Unity soporta texto enriquecido (https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html) de modo que se puede cambiar el color de un UnityEngine.UI.Text como se quiera, lo que evita tener que meter un game object por letra y cambiar cada una individualmente.~~

- Igual que tenéis el LevelManager al que llama el GameManager, deberíais haber tenido algo equivalente en las otras dos escenas.
    - Por ejemplo, vuestro componente SlotsScrollViewController busca al GameManager para preguntarle qué hacer, en lugar de que el flujo vaya al contrario.
    - Haciéndolo así, el GameManager tendría referencias al "controlador general de la escena" y al lanzar una escena se sabría dónde se está llegando en función de qué controlador tengamos configurado.

- El control del flujo general no es el descrito en clase.
    - Quién debería orquestar la ejecución global es el GameManager, como único objeto "Don't destroy on load".
    - Al cambiar de escena, el nuevo GameManager transfiere al singleton sus datos y desaparece (como hacéis)
    - Luego dependiendo del gestor que tenga (LevelManager en la escena de juego, SlotsScrollViewController en el menú, o lo que corresponda) sabe en qué escena está.
    - Y una vez que sabe en qué escena está, llama él para controlar al gestor de la escena correspondiente.
        - Por ejemplo, le manda al LevelManager la información del nivel a poner en marcha, y éste se lo manda al BoardManager.
    - Cuando el BoardManager ve que el jugador ha terminado, avisa al LevelManager.
    - El LevelManager avisa al GameManager para que guarde el progreso y luego muestra el anuncio, el cuadro de diálogo o lo que corresponda.
    - Cuando el LevelManager recibe la información de que el jugador quiere pasar al nivel siguiente no se configura a sí mismo (como hacéis en LevelManager::NextLevel()), sino que avisa al GameManager que es quién gestiona el flujo global.
    - El GameManager repite el ciclo, avisando de vuelta al LevelManager pasándole el nuevo nivel a poner en marcha.
    - Tenéis un GameManager principalmente pasivo que se dedica a esperar que los demás le ordenen en lugar de ser él quién sabe lo que está pasando y controla la ejecución.
        - Pero, por otro lado, hace algunas cosas que no debería. ¿Por qué muestra un banner en el Start? Eso debería ser decisión de las escenas. ¿Qué pasa si en una no queremos banner por alguna razón?

- En las propiedades de los componentes / scriptable objects no deberíais crear arrays. Por ejemplo en Theme::colorTheme o (peor aún) GameManager::colorsThemes. Es el usuario el que debería elegir el tamaño.
    - En el caso de Theme queremos forzar a que ponga 16, pero eso no se consigue haciendo el new y por tanto no aporta nada…

- Usáis Vector2 a lo largo y ancho de la lógica del tablero. Eso guarda números reales que no tiene sentido para referenciar tiles. Sería mejor Vector2Int o, mejor aún, vuestro propio tipo para no usar un tipo de Unity en la "lógica" hecha en "C# puro".

- ~~SlotButtonItem::Start() no debería registrarse como oyente de la pulsación del botón. Ya está hecho en el propio prefab.~~

- En la serialización no usáis pimienta en la hash.
- La serialización ocupa demasiado espacio. No es necesario guardar tanta información por nivel.
    - El mínimo número de movimientos con los que se puede resolver un nivel es el número de flujos. Sabiendo el menor número de movimientos con los que se ha resuelto un nivel es posible saber si tiene estrella o no. No necesitáis el campo perfect porque se puede calcular.
    - Eso significa que por cada nivel solo nos queda el campo bestMoves.
    - Y por tanto, no necesitamos una estructura por nivel. Para guardar el estado de un lote es suficiente un array de enteros.

- Es mala idea guardar el progreso usando la posición de los lotes y bloques en el array de datos del GameManager. Si se deciden añadir nuevos lotes el orden podría cambiar y el progreso de destroza. Cada lote debería tener un identificador y usar ese identificador como índice en el progreso.