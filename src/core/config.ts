export class Config {
    static Debug: boolean = true;
    
    public static GetUrl(): string {
        return localStorage.getItem('apiUrl') || (window.config || {}).ApiUrl || 'http://localhost:8080';
    }

    public static BuildUrl(url: string, params: any = null): string {
        let baseUrl: string = Config.GetUrl();

        let temp: string = `${baseUrl.replace(/^[\\|\/]+|[\\|\/]+$/g, '')}/${url.replace(/^[\\|\/]+|[\\|\/]+$/g, '')}`;
        let result: URL = new URL(temp);
        if (params) {
            Object.keys(params).forEach(key => params[key] && result.searchParams.append(key, params[key]));
            return result.toString();
        }
        return result.toString();
    }
}