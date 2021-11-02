package es.ucm.gdv.blas.oses.carreau.lib.Engine.Interfaces;

import java.util.List;

public interface Input {
    public static class TouchEvent {
        public static final int TOUCH_DOWN = 0;
        public static final int TOUCH_UP = 1;
        public static final int TOUCH_DRAGGED = 2;
        public int type;
        public int x, y;
        public int pointer;
    }
    public List<TouchEvent> getTouchEvents();
}