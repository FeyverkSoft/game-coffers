import * as React from 'react';
import style from "./input.module.less";
import { BaseInput, SmallSpinner } from '..';
import { IF, getGuid } from '../../_helpers';

export class Item {
    value: string | number;
    name: string;
    constructor(value: string | number, name?: string | number) {
        this.value = value;
        this.name = (name || value).toString();
    }
    get Key(): string {
        return `${this.value || this.name || getGuid()}`;
    }
    get key() {
        return this.Key;
    }
}

interface IMaterialSelectProps extends React.Props<any> {
    style?: React.CSSProperties;
    type: 'white' | 'default' | 'error';
    value?: number | string;
    path?: string;
    items?: Array<Item>;
    label?: string;
    id?: string;
    className?: string;
    isLoading?: boolean;
    onChange?(value: string, isValid?: boolean, path?: string): any;
    [id: string]: any;
}

export class MaterialSelect extends React.Component<IMaterialSelectProps, any> {
    constructor(props: IMaterialSelectProps) {
        super(props);
        this.state = {
            value: props.value || '',
            items: [new Item('<!--!>', ' '), ...(props.items || [])] || [],
            path: props.path
        };
        this.onChange = this.onChange.bind(this);
    }

    componentWillReceiveProps(props: IMaterialSelectProps) {
        const nP = [new Item('<!--!>', ' '), ...(props.items || [])];
        if (this.state.value != (props.value || '') ||
            nP != this.state.items ||
            this.state.path != props.path
        ) {
            this.setState({
                value: props.value || '',
                items: nP || [],
                path: props.path
            });
        }
    }

    onChange(val: React.ChangeEvent<HTMLSelectElement>) {
        let $this = this;
        let value = val.target.value;
        if (value != '<!--!>')
            if (value != $this.state.value) {
                $this.setState({
                    value: value
                });
                if ($this.props.onChange)
                    $this.props.onChange(value, true, $this.state.path);
            }
    }
    render() {
        let $this = this;
        let label;
        if ($this.props.label)
            label = <label>{$this.props.label}</label>;
        return (
            <div className={`${style['select-wrapper']} ${this.props.className || ''} ${style[this.props.type]}`}
                style={$this.props.style}>
                {label}
                <select
                    id={$this.props.id}
                    name={$this.props.id}
                    onChange={$this.onChange}
                    data-path={$this.state.path}
                    value={$this.state.value}>
                    {
                        $this.state.items.map((item: Item) => {
                            return <option key={item.Key} value={item.value}>{item.name || item.value || item} </option>;
                        })
                    }
                </select>
                <span className={style['bar']} />
                <IF value={this.props.isLoading}>
                    <SmallSpinner className="dark" />
                </IF>
            </div>
        )
    }

}
