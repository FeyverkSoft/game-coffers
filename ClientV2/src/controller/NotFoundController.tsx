import * as React from 'react';
import { LangF, Lang } from '../_services';
import { Card, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import style from './auth.module.scss';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';

export class NotFoundController extends React.Component {
    render() {
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <HomeOutlined />
                        <Link to={"/"} />
                    </Breadcrumb.Item>
                    <Breadcrumb.Item href="">
                        {Lang("PAGE_NOT_FOUND")}
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['auth']}>
                    <Card
                        title={LangF("NOT_FOUND", '')}
                    >
                        😏
                </Card>
                </div>
            </Content>
        );
    }
}