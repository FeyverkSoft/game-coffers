import * as React from 'react';
import style from "./pagination.module.less";
import { getGuid } from '../../_helpers';


interface IPaginationProps extends React.Props<any> {
    TotalPages: number;
    CurrentPage: number;
    onSelectPage: Function;
}
///Пагинатор
export class Pagination extends React.Component<IPaginationProps, any> {
    state: any;
    constructor(props: IPaginationProps) {
        super(props);
        let TotalPages = Math.floor(props.TotalPages);
        this.state = {
            Count: TotalPages > 0 ? TotalPages : 1,
            CurrentPage: props.CurrentPage
        };
        this.selectedPage = this.selectedPage.bind(this);
    }
    componentWillReceiveProps(props: IPaginationProps) {
        let TotalPages = Math.floor(props.TotalPages);
        this.setState({
            Count: TotalPages > 0 ? TotalPages : 1,
            CurrentPage: props.CurrentPage
        });
    }
    //надо бы переписать эту дичь :D
    getItems() {
        class items {
            index: number;
            body: any;
            className: string;
            constructor(index: number, body: any, className: string = '') {
                this.index = index;
                this.body = body;
                this.className = className;
            }
        }
        let $this = this;
        let arr: Array<any> = [];
        for (let i = 1; i <= this.state.Count; i++) {
            if ($this.state.CurrentPage == i + 1 || $this.state.CurrentPage == i - 1) {
                arr = arr.concat(new items(i, i))
            } else {
                if ($this.state.CurrentPage == i) {
                    arr = arr.concat(new items(i, i, 'active'));
                    console.log(style);
                }
                else {
                    if (i == 1 || i == this.state.Count)
                        arr = arr.concat(new items(i, i));
                }
            }
        }
        var unique: Array<any> = [];
        var distinct: Array<any> = [];
        for (var i in arr) {
            if (typeof (unique[arr[i].index]) == "undefined" && arr[i].index) {
                distinct.push(arr[i]);
            }
            unique[arr[i].index] = 0;
        }
        arr = distinct;
        if ($this.state.CurrentPage > 1)
            arr = [new items($this.state.CurrentPage - 1, '<', 'material-icon')].concat(arr);
        if ($this.state.CurrentPage < $this.state.Count)
            arr.push(new items(1 + parseInt($this.state.CurrentPage), '>', 'material-icon'));

        let result: Array<any> = [];
        for (let i = 0; i < arr.length; i++) {
            result = result.concat(arr[i]);
            if (arr[i + 1] && arr[i + 1].index)
                if ((Math.abs(arr[i + 1].index) - Math.abs(arr[i].index)) > 1) {
                    if (arr[i - 1] && arr[i - 1].index != -1)
                        result = result.concat(new items(-1, '...', 'material-icon'));
                }
        }
        return result;
    }

    selectedPage(e: React.MouseEvent<HTMLDivElement>) {
        if (e && e.currentTarget && e.currentTarget.id && e.currentTarget.id != '-1' && e.currentTarget.id != this.state.CurrentPage) {
            let currentPage = parseInt(e.currentTarget.id);
            this.setState({ CurrentPage: currentPage });
            if (this.props.onSelectPage)
                this.props.onSelectPage(currentPage);
        }
    }

    render() {
        let $this = this;
        let items = $this.getItems();
        return (
            <div className={style["pagination"]}>
                {items.map(x => {
                    return (
                        <div key={getGuid()}
                            className={`${style['pg']} ${x.className ? style[x.className] : ''}`}
                            onClick={$this.selectedPage}
                            id={x.index || -1}>{x.body}</div>
                    )
                })}
            </div>);
    }
}