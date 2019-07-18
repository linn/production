import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import AteFaultCodes from '../../components/ate/AteFaultCodes';
import ateFaultCodesActions from '../../actions/ateFaultCodesActions';
import ateFaultCodesSelectors from '../../selectors/ateFaultCodesSelectors';

const mapStateToProps = state => ({
    items: ateFaultCodesSelectors.getItems(state),
    loading: ateFaultCodesSelectors.getLoading(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = () => dispatch => {
    dispatch(ateFaultCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AteFaultCodes));
