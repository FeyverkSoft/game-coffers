const locString: any = {
    en: {
        NOT_FOUND: 'Resource {0} not found;',
        NOT_IMPLEMENTED: 'Action or function {0} not implemented',
        INTERNAL_SERVER_ERROR: 'Internal server error {0}',
        UNAUTHORIZED: 'Unauthorized. {0}',
        INCORRECT_PASSWORD: 'incorrect username or password. {0}',
        POINT_NOT_FOUND: 'Point {0} not found',
        INVALID_ARGUMENT: 'invalid argument {0}',
        RESOURCE_NOT_FOUND: 'Resource {0} not found;'
    },
    ru: {
        NOT_FOUND: 'Ресурс {0} не найден;',
        NOT_IMPLEMENTED: 'Метод или функционал {0} не реализован',
        INTERNAL_SERVER_ERROR: 'Произошла внутреняя ошибка сервера: {0}',
        UNAUTHORIZED: 'Доступ запрещён. {0}',
        INCORRECT_PASSWORD: 'Введён не верный логин и/или пароль. {0}',
        POINT_NOT_FOUND: 'Точка {0} не найдена',
        INVALID_ARGUMENT: 'Передан недопустимый аргумент {0}',
        RESOURCE_NOT_FOUND: 'Ресурс {0} не найден;'
    }
};


export const getCookie = function getCookie(name: string): string|undefined {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

export const setCookie = function setCookie(name: string, val: string): void {
    document.cookie = `${name}=${val}; path=../`;
}

export const CurrentLang = function (): string {
    return getCookie("Lang") || 'ru';
}

function getTranslate(value: string): string {
    if (!value)
        return value;
    var lang = CurrentLang();
    var locStr: any = {};
    if (locString[lang])
        locStr = locString[lang];
    else {
        locStr = locString["ru"];
        setCookie("Lang", "ru");
    }

    let lowerVal = value.replace(/ /gi, '').toLowerCase();

    let loc = locStr[value] || locStr[lowerVal];
    if (loc)
        return loc;

    for (let key in locStr) {
        if (key.toLowerCase() == lowerVal)
            return locStr[key];
    }
    return locString["ru"][lowerVal] || locString["ru"][value];
}

export const LangF = function (value: string, ...arg: (string | number)[]) {
    return Lang(value).format(...arg);
}

export const Lang = function (value: string, count?: number | undefined): string {
    let res: any;
    if (value)
        res = getTranslate(value) || value;
    else
        res = getTranslate(CurrentLang()) || value;

    if (count && res instanceof Object) {
        let keys = Object.keys(res).sort();
        if (res[count])
            return res[count].replace(/\{0\}/ig, count);

        for (let i = 0; i < keys.length; i++) {
            if (keys[i] > count.toString())
                return res[keys[i - 1]].replace(/\{0\}/ig, count);
        }
        return res[keys[0]].replace(/\{0\}/ig, count);
    }

    if (count)
        return res.replace(/\{0\}/ig, count);
    return res;
}