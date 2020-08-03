import * as React from 'react';
import { LoginForm } from '../_components/LoginForm/LoginForm';
import { Card, Breadcrumb, Tabs } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang } from '../_services';
import style from './auth.module.scss';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';

export default class AuthController extends React.Component {
    render() {
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
                        </Link>
                    </Breadcrumb.Item>
                    <Breadcrumb.Item>
                        <Link to={"/auth"} >
                            {Lang("PAGE_AUTH")}
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['auth']}>
                    <Card>
                        <Tabs defaultActiveKey="1"         style={{ width: '500px' }}>
                            <Tabs.TabPane tab={Lang("AUTHORIZE_FORM")} key="1">
                                <LoginForm />
                            </Tabs.TabPane>
                            <Tabs.TabPane tab={Lang("REG_IN_PUBLIC")} key="2">
                            </Tabs.TabPane>
                        </Tabs>
                    </Card>
                </div>
            </Content>
        );
    }
}