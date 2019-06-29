export interface ICallback<T=any> {
    onSuccess?(data?: T): void;
    onFailure?(data?: T): void;
}