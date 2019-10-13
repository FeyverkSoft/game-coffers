import * as React from 'react';
import style from "./input.module.less";
import { BaseInput, IBaseInputProps } from '..';
import { Lang } from '../../_services';

interface SearchInputProps extends IBaseInputProps { }
export class SearchInput extends BaseInput<SearchInputProps> {
    render() {
        return (
            <div className={style['search-wrapper']}>
                <div className={style['search-icon']} />
                <input className={style['search-input']}
                    placeholder={Lang('search')}
                    onChange={this.onChange}>
                </input>
                <span className={style['bar']} />
            </div>
        );
    }
}
