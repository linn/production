import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import { Page, getRequestErrors } from '@linn-it/linn-form-components-library';
import config from '../config';

const mapStateToProps = (state, ownProps) => ({
    requestErrors: getRequestErrors(state),
    showRequestErrors: ownProps.showRequestErrors,
    homeUrl: config.appRoot,
    width: ownProps.width
});

const mapDispatchToProps = {};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Page));
