import * as React from 'react';
import style from "./input.module.less";
import { SmallSpinner } from '..';
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

class _MaterialSelect extends React.Component<IMaterialSelectProps, any> {
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
        return (
            <div className={`${style['select-wrapper']} ${this.props.className}`}
                style={$this.props.style}>
                <select
                    className={`${style['input']} ${this.state.value ? `${style['full']} ${style['default']}` : style['error']}`}
                    id={this.props.path}
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
                <IF value={this.props.label}>
                    <label
                        className={`${style['label']} ${this.state.value ? style['default'] : style['error']}`}
                        htmlFor={this.props.path}
                    >
                        {$this.props.label}
                    </label>
                </IF>
                <span className={style['bar']} />
                <IF value={this.props.isLoading}>
                    <SmallSpinner className="dark" />
                </IF>
            </div>
        )
    }
}

export const MaterialSelect = React.memo(({ ...props }: IMaterialSelectProps) => <_MaterialSelect {...props} />)