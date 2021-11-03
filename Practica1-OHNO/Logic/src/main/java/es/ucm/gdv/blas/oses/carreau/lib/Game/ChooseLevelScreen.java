package es.ucm.gdv.blas.oses.carreau.lib.Game;

import java.awt.Color;
import java.util.ArrayList;
import java.util.List;

import es.ucm.gdv.blas.oses.carreau.lib.Assets;
import es.ucm.gdv.blas.oses.carreau.lib.Engine.Interfaces.Engine;
import es.ucm.gdv.blas.oses.carreau.lib.Engine.Interfaces.Graphics;
import es.ucm.gdv.blas.oses.carreau.lib.Engine.Interfaces.Input;
import es.ucm.gdv.blas.oses.carreau.lib.Engine.Screen;
import es.ucm.gdv.blas.oses.carreau.lib.Pair;

public class ChooseLevelScreen implements Screen {

    private Engine engine;
    private List<Pair<Integer, Integer>> celdas;

    public ChooseLevelScreen(Engine eng) {
        this.engine = eng;
        init();
    }

    @Override
    public void init() {
        celdas=new ArrayList<>();
        Graphics g = engine.getGraphics();
        int radio = (g.getLogWidth() / 5) / 2;
        for (int i = 0; i < 6; i++) {
            int x = g.getLogWidth() / 5 + radio + ((i % 3) * radio * 2);
            int y = 2 * g.getLogHeight() / 5 + (i / 3) * (radio * 2) + radio;
            celdas.add(new Pair<Integer, Integer>(x, y));
        }
    }

    @Override
    public void update(double deltaTime) {

    }


    @Override
    public void render() {
        Graphics g = engine.getGraphics();
        g.clear(0XFFFFFFFF);
        //Eleccion
        g.setColor(0x000000FF);
        g.drawText("Oh NO", Assets.molleregular, g.getLogWidth() / 2, g.getLogHeight() / 6,Assets.molleregular.getFontSize());
        g.drawText("Elija el tamaño a jugar ", Assets.josefisans, g.getLogWidth() / 2, g.getLogHeight() / 2 - (int) Assets.josefisans.getFontSize() * 2,(int)(Assets.josefisans.getFontSize() /1.5));

        int radio = (g.getLogWidth() / 5) / 2;
        for (int i = 0; i < celdas.size(); i++) {
            if (i % 2 == 0) g.setColor(0xFF0000FF);
            else g.setColor(0x0000FFFF);

            g.fillCircle(celdas.get(i).getLeft(), celdas.get(i).getRight(), radio);
            g.setColor(0xFFFFFFFF);
            g.drawText(Integer.toString(i + 4), Assets.josefisans,celdas.get(i).getLeft(), celdas.get(i).getRight()+ radio / 2,Assets.josefisans.getFontSize());
        }
        g.drawImage(Assets.close, g.getLogWidth() / 2 - Assets.close.getWidth() / 2, g.getLogHeight() - Assets.close.getHeight(), Assets.close.getWidth(), Assets.close.getHeight());
    }

    @Override
    public void handleEvents() {
        Graphics g = engine.getGraphics();
        List<Input.TouchEvent> touchEvents = engine.getInput().getTouchEvents();
        int len = touchEvents.size();
        for (int i = 0; i < len; i++) {
            System.out.println("procesando eventos");
            Input.TouchEvent event = touchEvents.get(i);
            if (event.type == Input.TouchEvent.TOUCH_UP) {
                System.out.println("evento TOUCH_UP");
                int radio = (g.getLogWidth() / 5) / 2;

                for (int j = 0; j < celdas.size(); j++) {
                    if (inBounds(event,  celdas.get(j).getLeft() - radio, celdas.get(j).getRight() - radio, radio*2, radio*2))
                    {
                        engine.setScreen(new GameScreen(engine, j + 4));
                    }
                }
            }
        }
    }


    private boolean inBounds(Input.TouchEvent event, int x, int y, int width, int height) {
        if (event.x > x && event.x < x + width - 1 &&
                event.y > y && event.y < y + height - 1)
            return true;
        else
            return false;
    }
}
