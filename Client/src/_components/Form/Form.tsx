import * as React from "react";
import style from "./form.module.less";

interface IFormProps extends React.Props<any> {
    onSubmit?(e?: React.FormEvent<HTMLFormElement>): any,
    className?: string;
    direction?: 'vertical' | 'horizontal';
    [id: string]: any;
}
export class Form extends React.Component<IFormProps> {

    constructor(props: IFormProps) {
        super(props);
    }

    handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (this.props.onSubmit)
            this.props.onSubmit(e);
    }

    render() {
        let { onSubmit, ...props } = this.props;
        return (
            <form onSubmit={this.handleSubmit}
                className={`${style['form']} ${style[props.direction || 'horizontal']}`}
                {...props}>
                {this.props.children}
            </form>
        );
    }
}