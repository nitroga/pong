global using Raylib_cs;
global using System.Numerics;

int screenWidth = 1600;
int screenHeight = 900;

Raylib.InitWindow(screenWidth, screenHeight, "Bong :)");

Raylib.SetTargetFPS(144);

Random rnd = new Random();
Vector2 mousePos;
Vector2 ballPos = new Vector2(screenWidth/2, screenHeight/2);
Vector2 rectPos = new Vector2(0, 375);
Vector2 rect1Pos = new Vector2(screenWidth-30, 375);
Vector2 rectSize = new Vector2(30, 150);
int ballSpeedX = 3;
int ballSpeedY = 0;
int score = 0;

while(!Raylib.WindowShouldClose()) {
    mousePos = Raylib.GetMousePosition();
    ballPos.X+=ballSpeedX;
    ballPos.Y+=ballSpeedY;
    Rectangle rect = new Rectangle(rectPos.X, rectPos.Y, rectSize.X, rectSize.Y);
    Rectangle rect1 = new Rectangle(rect1Pos.X, rect1Pos.Y, rectSize.X, rectSize.Y);

    if (ballSpeedY == 0) {
        ballSpeedY = rnd.Next(-2, 2);
    }

    if (ballPos.X >= screenWidth) {
        ballSpeedX = -3;
        ballSpeedY = rnd.Next(-2, 2);
    }

    if (ballPos.Y >= screenHeight || ballPos.Y <= 0) {
        ballSpeedY = -ballSpeedY;
    }

    if (Raylib.CheckCollisionCircleRec(ballPos, 5, rect)) {
        ballSpeedX = 3;
        ballSpeedY = rnd.Next(-2, 2);
        score++;
    }

    if (Raylib.CheckCollisionCircleRec(ballPos, 5, rect1)) {
        ballSpeedX = -3;
        ballSpeedY = rnd.Next(-2, 2);
    }

    if (ballPos.X <= 18) {
        ballSpeedX = 0;
        ballSpeedY = 0;
        Raylib.DrawText("Game Over!", screenWidth/2-165, screenHeight/2, 60, Color.RED);
    }

    if (ballPos.X >= screenWidth - 18) {
        ballSpeedX = 0;
        ballSpeedY = 0;
        Raylib.DrawText("You win!", screenWidth/2-165, screenHeight/2, 60, Color.GREEN);
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) {
        rectPos.Y-=1.5f;
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) {
        rectPos.Y+=1.5f;
    }

    if (ballPos.Y > rect1Pos.Y) {
        rect1Pos.Y+=1.5f;
    }

    if (ballPos.Y < rect1Pos.Y) {
        rect1Pos.Y-=1.5f;
    }

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.BLACK);

    Raylib.DrawCircle((int)ballPos.X, (int)ballPos.Y, 20, Color.WHITE);

    Raylib.DrawText("Score", screenWidth/2-28, 0, 25, Color.WHITE);
    Raylib.DrawText($"{score}", screenWidth/2, 25, 35, Color.WHITE);

    Raylib.DrawRectangleRec(rect, Color.WHITE);
    Raylib.DrawRectangleRec(rect1, Color.WHITE);

    Raylib.EndDrawing();
}