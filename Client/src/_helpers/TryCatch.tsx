import * as React from 'react';

export class TryCatch extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = { hasError: false };
    }
    componentDidCatch(error: any, info: any) {
        this.setState({
            hasError: true,
            error: error,
            info: info
        });
    }
    render() {
        if (this.state.hasError) {
            return <div>
                <div>
                    O-oops!!! 😨
                </div>
                <div>
                    <pre style={{ whiteSpace: 'pre-line' }}>
                        {JSON.stringify(this.state.error)}
                    </pre>
                    <pre style={{ whiteSpace: 'pre-line' }}>
                        {JSON.stringify(this.state.info)}
                    </pre>
                </div>
            </div>;
        }
        return this.props.children;
    }
}