import * as React from 'react';
import { LangF } from '../_services';
import { Card } from 'antd';
import style from './auth.module.less';

export class NotFoundController extends React.Component {
    render() {
        return (
            <div className={style['auth']}>
                <Card
                    title={LangF("NOT_FOUND",'')}
                >
                   😏
                </Card>
            </div>
        );
    }
}