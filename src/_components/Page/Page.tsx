import React from "react";
import style from "./page.module.less";
import { IF } from "../../_helpers";
import { Spinner, Crumbs, Breadcrumbs } from "..";

interface IPageProps extends React.BaseHTMLAttributes<any> {
    pageActions?: React.ReactNode | string;
    breadcrumbs?: Array<Crumbs>;
    isLoading?: boolean;
}

export class Page extends React.Component<IPageProps, any> {
    constructor(props: IPageProps) {
        super(props);
        this.state = {};
    }
    render() {
        return (
            <div className={`${style['page']} ${this.props.className ? '' + this.props.className : ''}`}>
                <div className={`${style['title']} ${this.props.className ? '' + this.props.className : ''}`}>
                    <div>
                        <IF value={this.props.pageActions}>
                            {this.props.pageActions}
                        </IF>
                    </div>
                    <div>
                        {this.props.title}
                    </div>
                </div>
                <Breadcrumbs items={this.props.breadcrumbs || []} />
                <div className={style["content"]}>
                    <IF value={this.props.isLoading}>
                        <Spinner />
                    </IF>
                    {this.props.children}
                </div>
            </div>
        );
    }
}