import * as React from 'react';

interface IFProps extends React.Props<any> {
    value?: boolean | React.ReactNode;
}

export class IF extends React.Component<IFProps>
{
    render() {
        if (this.props.value)
            return this.props.children;
        else
            return null;
    }
}