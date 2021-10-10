package es.ucm.gdv.blas.oses.carreau.ohno_practica1.Game;
import java.util.List;
import es.ucm.gdv.blas.oses.carreau.ohno_practica1.Interfaces.Engine;
import es.ucm.gdv.blas.oses.carreau.ohno_practica1.Interfaces.Graphics;
import es.ucm.gdv.blas.oses.carreau.ohno_practica1.Interfaces.Input.TouchEvent;
import es.ucm.gdv.blas.oses.carreau.ohno_practica1.Screen;
import es.ucm.gdv.blas.oses.carreau.ohno_practica1.Assets;

public class MainMenuScreen extends Screen {

    public MainMenuScreen(Engine engine) {
        super(engine);
    }

    public void update(float deltaTime) {
        Graphics g = engine.getGraphics();
        List<TouchEvent> touchEvents = engine.getInput().getTouchEvents();
        //engine.getInput().getKeyEvents();
        int len = touchEvents.size();
        for (int i = 0; i < len; i++) {
            TouchEvent event = touchEvents.get(i);
            if (event.type == TouchEvent.TOUCH_UP) {
                if (inBounds(event, g.getWidth() / 3, g.getHeight() - 32, 96, 32)) {
                    engine.setScreen(new GameScreen(engine));
                    return;
                }
            }
        }
    }

    private boolean inBounds(TouchEvent event, int x, int y, int width, int height) {
        if(event.x > x && event.x < x + width - 1 &&
                event.y > y && event.y < y + height - 1)
            return true;
        else
            return false;
    }

    public void present(float deltaTime) {
        Graphics g = engine.getGraphics();
      //  g.clear(0);
        //ohno texto g.drawText()
        //Jugar texto g.drawText()
        //Descripcion texto g.drawText()
        g.drawImage(Assets.q42, 0, 0);

    }

    @Override
    public void pause() {

    }

    @Override
    public void resume() {

    }

    @Override
    public void dispose() {

    }
}

