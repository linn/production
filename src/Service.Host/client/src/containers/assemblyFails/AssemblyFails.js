import { connect } from 'react-redux';
import AssemblyFails from '../../components/assemblyFails/AssemblyFails';
import assemblyFailsActions from '../../actions/assemblyFailsActions';
import assemblyFailsSelectors from '../../selectors/assemblyFailsSelectors';

const mapStateToProps = state => ({
    items: assemblyFailsSelectors.getSearchItems(state),
    loading: assemblyFailsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: assemblyFailsActions.search,
    clearSearch: assemblyFailsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(AssemblyFails);
