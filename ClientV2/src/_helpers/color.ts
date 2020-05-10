export const hexToRgb = (hex: string): any => {
    const shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])([a-f\d])?$/i;
    hex = hex.replace(shorthandRegex, function (m, r, g, b, a) {
        return r + r + g + g + b + b + (a || '') + (a || '');
    });

    let result: RegExpExecArray | null = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})?$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16),
        a: parseInt(result[4], 16)
    } : null;
};

export const BlendColor = (start?: string, end?: string, proc?: number) => {
    if (!start || !end)
        return;
    if (!proc)
        proc = 0;
    if (proc > 100)
        proc = 100;
    const toHex = (c: number): string => {
        let hex: string = c.toString(16);
        return hex.length === 1 ? "0" + hex : hex;
    };
    const startRgb = hexToRgb(start);
    const endRgb = hexToRgb(end);
    let r: number = Math.round((endRgb.r - startRgb.r) * (proc / 100)) + startRgb.r;
    let g: number = Math.round((endRgb.g - startRgb.g) * (proc / 100)) + startRgb.g;
    let b: number = Math.round((endRgb.b - startRgb.b) * (proc / 100)) + startRgb.b;
    let a: number = Math.round(((endRgb.a || 255) - (startRgb.a || 255)) * (proc / 100)) + (startRgb.a || 255);

    if (/Edge/.test(navigator.userAgent))
        return `rgba(${r},${g},${b},${a})`;

    return `#${toHex(r)}${toHex(g)}${toHex(b)}${toHex(a)}`
};

export const getRandomColor = (): string => {
    const letters: string = '0123456789ABCDEF';
    let color: string = '#';
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
};
