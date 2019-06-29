import * as React from "react";
import style from "./spinner.module.less"

interface ISpinnerProps extends React.Props<any> {
    header?: React.ReactNode;
    title?: string;
    footer?: React.ReactNode;
    isLoading?: boolean;
    onDoubleClick?: Function;
    className?: string;
    [id: string]: any;
}
/// анимация загрузки
export class Spinner extends React.Component<ISpinnerProps> {
    onDoubleClick = () => {
        if (this.props.onDoubleClick) {
            this.props.onDoubleClick();
        }
    }
    render() {
        return (
            <div className={style["loading-overlay"]}
                onDoubleClick={this.onDoubleClick}>
                <div className={style["spinner"]}>
                    <div className={style["bounce1"]}></div>
                    <div className={style["bounce2"]}></div>
                    <div className={style["bounce3"]}></div>
                </div>
            </div>
        );
    }
}

///Анимация загрузки на кнопках итд
export const SmallSpinner = ({ ...props }: ISpinnerProps) => {
    return (
        props.isLoading == true || props.isLoading == undefined ?
            <div className={`${style['sm-spinner']} ${props.className ? style[props.className] : ''}`}>
                <div className={style['bounce1']}></div>
            </div> :
            null
    );
}