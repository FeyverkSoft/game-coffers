import * as React from 'react';
import { LoginForm } from '../_components/LoginForm/LoginForm';
import { Card, Button, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import { Lang } from '../_services';
import style from './auth.module.scss';
import { sessionInstance } from '../_actions/session.actions';
import { connect } from 'react-redux';
import { IStore } from '../_helpers/store';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';

interface LogOutControllerProps {
    isLoading: boolean;
    logOut: Function;
}
const _LogOutController = ({ ...props }: LogOutControllerProps) => {
    return (
        <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <Link to={"/"} >
                        <HomeOutlined />
                    </Link>
                </Breadcrumb.Item>
                <Breadcrumb.Item>
                    <Link to={"/logout"} >
                        {Lang("LOGOUT_PAGE")}
                    </Link>
                </Breadcrumb.Item>
            </Breadcrumb>
            <div className={style['auth']}>
                <Card
                    title={Lang("LOGOUT_PAGE_")}
                    style={{ width: '500px' }}
                >
                    <Button
                        type="primary"
                        htmlType="submit"
                        loading={props.isLoading}
                        onClick={() => props.logOut()}
                    >
                        {Lang('LogOut')}
                    </Button>
                </Card>
            </div>
        </Content>
    );
}

const connectedLogOutController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
        };
    },
    (dispatch: Function) => {
        return {
            logOut: () => dispatch(sessionInstance.logOut()),
        }
    })(_LogOutController);

export { connectedLogOutController as LogOutController };
