//причесать её потом
//вычисляет относительное заполнение
export const calcProcent = (min: number, max: number, current: number): number => {
    if (max <= 0 && min < 0 && current < 0)
        return (current / getInterval(max, min)) * -100;

    if (max > 0 && min < 0 && current < 0)
        return (current / getInterval(0, min)) * -100;

    if (min >= 0 && current >= 0 && max >= 0)
        return (Math.abs(min - current) / getInterval(max, min)) * 100;

    if (current > 0 && max >= 0 && min <= 0)
        return (current / getInterval(max, 0)) * 100;

    return Math.abs((min - current) / getInterval(max, min)) * 100;
}

const getInterval = (min: number, max: number): number => {
    let interval = Math.abs(max - min);
    return interval == 0 ? 1 : interval;
}