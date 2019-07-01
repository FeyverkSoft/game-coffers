interface String {
    format(...arg: (string|number)[]): string;
}

interface Array<T> {
    getIndex(func: Function): number;
    firstOrDefault(func: Function, def: T): T;
    diff(array: T[]): T[]
}