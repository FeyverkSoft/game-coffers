import * as React from 'react';
import { connect } from 'react-redux';
import { alertInstance } from '../_actions';
import { getGuid, IStore } from '../_helpers';
import { Lang } from '../_services';
import { Pagination, Spinner, SmallSpinner, Dialog, Button, CanvasBlock } from '../_components';

class Demo extends React.Component<any, any> {
    state = { dialog_isDisplayed: false };
    constructor(props: any) {
        super(props);
    }
    error = () => {
        const { dispatch } = this.props;
        dispatch(alertInstance.error(getGuid()))
    };
    info = () => {
        const { dispatch } = this.props;
        dispatch(alertInstance.info(getGuid()))
    };
    success = () => {
        const { dispatch } = this.props;
        dispatch(alertInstance.success(getGuid()))
    };
    clear = () => {
        const { dispatch } = this.props;
        dispatch(alertInstance.clear())
    };

    ShowDialog = (flag: boolean) => {
        this.setState({ dialog_isDisplayed: flag })
    }

    render() {
        return (<div>
            <div style={{ padding: '.3rem', display: 'flex' }}>
                {'Message test '}
                <Button onClick={this.error}
                    type="important">error message</Button>
                <Button onClick={this.info}
                    type="default">info message</Button>
                <Button onClick={this.success}
                    type="success">success message</Button>
                <Button onClick={this.clear}
                    type="none">clear message</Button>
                <Button onClick={this.clear}
                    type="none" isLoading={true}>loading</Button>
            </div>
            <div style={{ padding: '.3rem', display: 'flex' }}>
                {'Pagination test '}
                <Pagination
                    TotalPages={3}
                    CurrentPage={2}
                    onSelectPage={() => { }}
                />
            </div>
            <div style={{ padding: '.3rem', position: 'relative', height: '3rem', display: 'flex' }}>
                {'Spinner'}
                <Spinner />
            </div>
            <div style={{ padding: '.3rem', position: 'relative', height: '3rem', display: 'flex' }}>
                {'SmallSpinner'}
                <SmallSpinner />
            </div>
            <div style={{ padding: '.3rem', display: 'flex' }}>
                {'Dialog '}
                <Button onClick={() => this.ShowDialog(true)}>test dialog</Button>
                <Dialog
                    isDisplayed={this.state.dialog_isDisplayed}
                    onCancel={() => this.ShowDialog(false)}
                    title="sdsdsdsd sdsd"
                >
                    {"Привет мир!!!"}
                </Dialog>
            </div>
            <CanvasBlock
                type={'important'}
                isLoading={!true}
                title={'demo'}
            >
                <Pagination
                    TotalPages={3}
                    CurrentPage={2}
                    onSelectPage={() => { }}
                />
            </CanvasBlock>
        </div>);
    }
}

const DemoController = connect((state: IStore) => {
    const { session } = state;
    return {
        session
    };
})(Demo);

export default DemoController; 