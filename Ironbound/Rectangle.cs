namespace Ironbound {
    struct Rectangle {
        public int x, y, width, height;

        public Rectangle(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public bool Intersects(Rectangle other) {
            return
                (x <= other.x + other.width && x + width >= other.x) &&
                (y <= other.y + other.height && y + height >= other.y);
        }

        public bool Contains(Point p) => x <= p.x && x + width >= p.x && y <= p.y && y + width >= p.y;
    }
}
