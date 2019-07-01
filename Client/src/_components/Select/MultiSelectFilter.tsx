import * as React from 'react';
import { SmallSpinner } from '..';
import { getGuid } from '../../_helpers';


export class MultiItem {
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

interface IMultiSelectFilterProps {
    items: Array<MultiItem>;
    value?: string;
    path?: string;
    isLoading?: boolean;
    title?: string | React.ReactChild;
    tabIndex?: number;
    onSelect(value?: string, valid?: boolean, path?: string): any;
}

interface IState {
    selectedItems: string[];
    isOpened: boolean;
    items: Array<MultiItem>;
}

export class MultiSelectFilter extends React.Component<IMultiSelectFilterProps, IState> {
    constructor(props: any) {
        super(props);
        this.state = {
            selectedItems: [],
            isOpened: false,
            items: this.props.items || []
        }
    }

    componentWillReceiveProps(props: IMultiSelectFilterProps) {
        let items = props.items || this.state.items || [];
        let selectedItems = ((props.value || '').split(',') || this.state.selectedItems || []).filter(_ => _ && items.filter(x => x.value == _).length !== 0);
        this.setState({
            items: items,
            selectedItems: selectedItems
        });
    }

    toggle = (force: any) => {
        if (typeof (force) == typeof (true))
            this.setState({ isOpened: force })
        else
            this.setState({ isOpened: !this.state.isOpened })
    }


    onClickItem = (event: any) => {
        event.preventDefault();
        let id = event.target.dataset['key'];
        let selectedItems = [];
        if (this.state.selectedItems.some(_ => _ == id)) {
            selectedItems = this.state.selectedItems.filter(_ => _ != id);
        } else {
            selectedItems = Array().concat(this.state.selectedItems, this.state.items.filter(_ => _.value == id)[0].value);
        };
        this.setState({
            selectedItems: selectedItems
        });
        if (this.props.onSelect) {
            this.props.onSelect(selectedItems.join(','), true, this.props.path);
        }
    }

    itemsView = () => {
        if (!this.state.isOpened)
            return;
        const $this = this;
        return (
            <ul className="item-wrapper">
                {this.state.items.map(x => {
                    return <li className={`items${$this.state.selectedItems.some(_ => _ == x.value) ? ' selected' : ''}`}
                        key={x.key}
                        onClick={$this.onClickItem}
                        data-key={x.value}
                    >
                        {x.name}
                    </li>
                })}
            </ul>
        );
    }

    selectedItemsView = () => {
        if ((this.state.selectedItems || []).length == 0)
            return;

        return (
            <div className={`selected-items${this.state.isOpened ? ' open' : ''}`}
                onClick={this.toggle}
            >
                <div className='label blue'>{this.state.selectedItems.length}</div>
            </div>
        );
    }

    loadingView = () => {
        if (this.props.isLoading)
            return (
                <div className="overlay">
                    <SmallSpinner className="dark" />
                </div>
            );
    }

    render() {
        return (
            <div className="multi-select"
                onBlur={() => this.toggle(false)}
                tabIndex={this.props.tabIndex}
            >
                {this.loadingView()}
                <div className='content'>
                    <label className={`title${this.state.isOpened ? ' open' : ''}`}
                        onClick={this.toggle}
                    >
                        {this.props.title}
                    </label>
                    {this.selectedItemsView()}
                    {this.itemsView()}
                </div>
                <div className='icon'
                    onClick={this.toggle}
                    onKeyUp={(event) => { if (event.which == 13) this.toggle('') }}
                    tabIndex={this.props.tabIndex}
                >
                    <span className={`mdi mdi-menu-${this.state.isOpened ? 'up' : 'down'}`}></span>
                </div>
            </div>
        );
    }
}