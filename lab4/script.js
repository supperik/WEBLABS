const canvas = document.getElementById("gameCanvas");
    const ctx = canvas.getContext("2d");
    
    // Размеры ячейки и лабиринта
    const cellSize = 20;
    const mazeWidth = canvas.width / cellSize;
    const mazeHeight = canvas.height / cellSize;
    
    // Генерируем лабиринт
    const [maze, startX, startY] = generateMaze();
    
    // Начальные координаты игрока
    let playerX = startX;
    let playerY = startY;
    
    // Слушаем события клавиатуры
    window.addEventListener("keydown", movePlayer);
    
    // Отрисовываем игру
    function draw() {
        // Очищаем холст
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        
        // Отрисовываем лабиринт
        maze.forEach((row, y) => {
            row.forEach((cell, x) => {
                if (cell === 1) {
                    ctx.fillStyle = "#000000";
                    ctx.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
                } else if (cell === 2) {
                    ctx.fillStyle = "#00FF00"; // Зеленый цвет для конечной точки
                    ctx.fillRect(x * cellSize, y * cellSize, cellSize, cellSize);
                }
            });
        });
        
        // Отрисовываем игрока
        ctx.fillStyle = "#FF0000";
        ctx.fillRect(playerX * cellSize, playerY * cellSize, cellSize, cellSize);
    }

    // Функция для перемещения игрока
    function movePlayer(event) {
        const key = event.key;
        let newX = playerX;
        let newY = playerY;
        
        switch (key) {
            case "ArrowUp", "w":
                newY = Math.max(0, playerY - 1);
                break;
            case "ArrowDown", "s":
                newY = Math.min(mazeHeight - 1, playerY + 1);
                break;
            case "ArrowLeft", "a":
                newX = Math.max(0, playerX - 1);
                break;
            case "ArrowRight", "d":
                newX = Math.min(mazeWidth - 1, playerX + 1);
                break;
            default:
                return;
        }
        
        // Проверяем, можно ли переместить игрока
        if (maze[newY][newX] === 0) {
            playerX = newX;
            playerY = newY;
            draw();
        }

        // Проверяем, наступил ли игрок на зеленую клетку (конец лабиринта)
        if (maze[newY][newX] === 2) {
            // Останавливаем игру и выводим сообщение о победе
            window.removeEventListener("keydown", movePlayer); // Удаляем обработчик клавиатуры
            alert("Поздравляем! Вы достигли выхода из лабиринта!");
            location.reload();
        }
        
    }
    
    // Функция для генерации лабиринта
    function generateMaze() {
        // Создаем пустой лабиринт
        const maze = Array.from({ length: mazeHeight - 1 }, () => Array.from({ length: mazeWidth - 1 }, () => 1));
        
        // Рекурсивная функция для создания пути в лабиринте
        function createPath(x, y) {
            maze[y][x] = 0; // Отмечаем текущую ячейку как проход
            
            // Создаем список смежных ячеек в случайном порядке
            const directions = ["up", "down", "left", "right"].sort(() => Math.random() - 0.5);
            
            // Для каждого направления пытаемся пробить стену
            for (const direction of directions) {
                let newX = x;
                let newY = y;
                switch (direction) {
                    case "up":
                        newY -= 2;
                        break;
                    case "down":
                        newY += 2;
                        break;
                    case "left":
                        newX -= 2;
                        break;
                    case "right":
                        newX += 2;
                        break;
                }
                
                // Проверяем, что новая ячейка находится в пределах лабиринта и еще не была посещена
                if (newX > 0 && newX < mazeWidth - 1 && newY > 0 && newY < mazeHeight - 1 && maze[newY][newX] === 1) {
                    maze[y + (newY - y) / 2][x + (newX - x) / 2] = 0; // Отмечаем ячейку между текущей и новой как проход
                    createPath(newX, newY); // Рекурсивно вызываем функцию для продолжения пути
                }
            }
        }
        
        // Начинаем генерацию лабиринта из случайной стартовой ячейки
        const startX = 1;
        const startY = 1;
        createPath(startX, startY);
        
        // Находим случайный проход вокруг периметра лабиринта и помечаем его как выход
        const perimeter = [
            { x: mazeWidth - 2, y: 1 },
            { x: 1, y: mazeHeight - 2 },
            { x: mazeWidth - 3, y: mazeHeight - 3}
        ];

        const exit = perimeter[Math.floor(Math.random() * perimeter.length)];
        maze[exit.y][exit.x] = 2; // Помечаем конечную точку как выход из лабиринта
        console.log(exit)
        // Возвращаем сгенерированный лабиринт
        return [maze, startX, startY];
    }
    
    // Отрисовываем начальное состояние игры
    draw();