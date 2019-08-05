import * as React from 'react';
import style from "./input.module.less";
import { Item, Private } from '..';
import { IF } from '../../_helpers';

interface IEditableListProps {
    items: Array<Item>;
    value: string;
    roles: Array<string>;
    onSave(value: string): void;
}
class _EditableList extends React.Component<IEditableListProps, any> {
    constructor(props: IEditableListProps) {
        super(props);
        this.state = {
            value: props.value || '',
            items: [new Item('<!--!>', ' '), ...(props.items || [])] || [],
            edited: false
        };
    }

    componentWillReceiveProps(props: IEditableListProps) {
        const nP = [new Item('<!--!>', ' '), ...(props.items || [])];
        if (this.state.value != (props.value || '') ||
            nP != this.state.items
        ) {
            this.setState({
                value: props.value || '',
                items: nP || [],
            });
        }
    }

    onChange = (val: React.ChangeEvent<HTMLSelectElement>) => {
        let $this = this;
        let value = val.target.value;
        if (value != '<!--!>')
            if (value != $this.state.value) {
                $this.setState({
                    value: value
                });
            }
    }

    onEdit = () => {
        this.setState({ edited: true });
    }

    onSave = () => {
        this.props.onSave(this.state.value);
        this.setState({ edited: false });
    }

    render() {
        const $this = this;
        return (
            <div className={style['editable-list']}>
                <IF value={!this.state.edited}>
                    {this.props.items.filter(x => String(x.value).toLowerCase() == this.props.value.toLowerCase())[0].name}
                    <Private roles={['admin', 'leader', 'officer']}>
                        <span className={style['edit-icon']} onClick={() => this.onEdit()} />
                    </Private>
                </IF>
                <IF value={this.state.edited}>
                    <select
                        className={style['select']}
                        onChange={$this.onChange}
                        value={$this.state.value}>
                        {
                            $this.state.items.map((item: Item) => {
                                return <option key={item.Key} value={item.value}>{item.name || item.value || item} </option>;
                            })
                        }
                    </select>
                    <div className={style['save-icon']}
                        onClick={(e) => this.onSave()}>
                    </div>
                </IF>
            </div >
        )
    }
}

export const EditableList = React.memo(({ ...props }: IEditableListProps) => <_EditableList {...props} />)