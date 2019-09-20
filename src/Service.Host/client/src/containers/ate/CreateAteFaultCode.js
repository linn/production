import { connect } from 'react-redux';
import { getItemError, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ateFaultCodeActions from '../../actions/ateFaultCodeActions';
import AteFaultCode from '../../components/ate/AteFaultCode';
import ateFaultCodeSelectors from '../../selectors/ateFaultCodeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    itemError: getItemError(state, itemTypes.ateFaultCode.item),
    loading: ateFaultCodeSelectors.getLoading(state),
    snackbarVisible: ateFaultCodeSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(ateFaultCodeActions.setEditStatus('create'));
    dispatch(ateFaultCodeActions.create());
};

const mapDispatchToProps = {
    initialise,
    addItem: ateFaultCodeActions.add,
    setEditStatus: ateFaultCodeActions.setEditStatus,
    setSnackbarVisible: ateFaultCodeActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AteFaultCode));
