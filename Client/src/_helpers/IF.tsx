import * as React from 'react';

interface IFProps<T = any> extends React.Props<any> {
    value?: boolean | React.ReactNode | T;
    in?: Array<T>;
}

class _IF extends React.Component<IFProps>
{
    render() {
        if (this.props.in && this.props.in.length > 0) {
            if (this.props.in.includes(this.props.value))
                return this.props.children;
            return null;
        }
        if (this.props.value)
            return this.props.children;
        else
            return null;
    }
}

export const IF = React.memo(({ ...props }: IFProps) => <_IF {...props} />);