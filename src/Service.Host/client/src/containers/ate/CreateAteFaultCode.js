import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ateFaultCodeActions from '../../actions/ateFaultCodeActions';
import AteFaultCode from '../../components/ate/AteFaultCode';
import ateFaultCodeSelectors from '../../selectors/ateFaultCodeSelectors';

const mapStateToProps = state => ({
    item: {},
    editStatus: 'create',
    errorMessage: fetchErrorSelectors(state),
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
