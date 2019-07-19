import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AteFaultCode from '../../components/ate/AteFaultCode';
import ateFaultCodeActions from '../../actions/ateFaultCodeActions';
import ateFaultCodeSelectors from '../../selectors/ateFaultCodeSelectors';

const mapStateToProps = (state, { match }) => ({
    item: ateFaultCodeSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: ateFaultCodeSelectors.getEditStatus(state),
    loading: ateFaultCodeSelectors.getLoading(state),
    snackbarVisible: ateFaultCodeSelectors.getSnackbarVisible(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(ateFaultCodeActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    updateItem: ateFaultCodeActions.update,
    setEditStatus: ateFaultCodeActions.setEditStatus,
    setSnackbarVisible: ateFaultCodeActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AteFaultCode));
